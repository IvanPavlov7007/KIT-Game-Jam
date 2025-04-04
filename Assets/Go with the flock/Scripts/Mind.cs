using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Mind : MonoBehaviour
{
    //implement when to callback decision!!

    public virtual Flock flock { get; protected set; }
    public Process process { get; private set; }

    protected Action<Mind, ProcessDecision> decisionCallback;
    public virtual void reportCollision(FlockingMember other)
    {
        if (process != null)
            return;

        Mind otherMind = other.Mind;
        if (otherMind == null)
            throw new UnityException("Animal without mind: " + other.name);
        if (otherMind == this)
            return;
        if (otherMind.process != null)
            return;

        GameManager.Instance.initNewProcess(this, otherMind);
    }

    public virtual void StartProcess(Process process, Action<Mind, ProcessDecision> callBack)
    {
        this.process = process;
        decisionCallback = callBack;
    }

    public virtual void StopProcess()
    {
        this.process = null;
        decisionCallback = null;
    }

    public virtual void killFlock()
    {
        flock.kill();
    }

    public virtual FightResult SufferFight(Mind enemy) 
    {
        var enemyStats = enemy.getStats();

        int damage = enemyStats.attack + enemyStats.additionalAttack;
        bool shouldBeDestroyed = false;

        var thisStats = getStats();
        var otherStats = enemy.getStats();

        thisStats.health -= otherStats.attack + otherStats.additionalAttack;
        shouldBeDestroyed = thisStats.health <= 0;

        return new FightResult(this, shouldBeDestroyed, applyDamage, damage);
    }

    public virtual void applyDamage(FightResult result)
    {
        flock.DistributeDamage(result.damage);

    }

    

    public virtual StatsCollection getStats()
    {
        var stats = flock.stats;
        if (flock.animalsInFlock.Count == 1)
        {
            stats = flock.animalsInFlock[0].stats;
        }
        return stats;
    }

    public virtual void initializeMind(FlockingMember flockingMember)
    {
        flock = gameObject.AddComponent<Flock>();
        flock.initializeFlock(flockingMember);
    }



    public void growFlock(Flock otherFlock)
    {
        this.flock.addFlockMembers(otherFlock.animalsInFlock);
        foreach (var member in otherFlock.animalsInFlock)
        {
            member.Mind = this;
        }
    }

    public virtual void DestroyMind()
    {
        Destroy(gameObject);
    }
}

public struct FightResult
{
    public Mind mind;
    public bool shouldBeDestroyed;
    public System.Action<FightResult> applySufferChanges;
    public int damage;

    public FightResult(Mind mind, bool shouldBeDestroyed, Action<FightResult> applySufferChanges, int damage)
    {
        this.mind = mind;
        this.shouldBeDestroyed = shouldBeDestroyed;
        this.applySufferChanges = applySufferChanges;
        this.damage = damage;
    }
}