using System.Collections;
using UnityEngine;
using System;
public class Mind : MonoBehaviour
{
    [SerializeField]
    Flock flock;
    public Process process { get; private set; }
    public StatsEntity statsEntity { get; protected set; }

    protected Action<Mind, ProcessDecision> decisionCallback;
    public virtual void reportCollision(FlockingMember other)
    {
        if (process != null)
            return;

        Mind otherMind = other.;
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
        //TODO call callback!!
    }

    public virtual void StopProcess()
    {
        this.process = null;
        decisionCallback = null;
    }

    public virtual FightResult SufferFight() {
    }

    public void growFlock(Flock flock)
    {

    }
}

public struct FightResult
{
    Mind mind;
    bool shouldBeDestroyed; 
    System.Action<Mind> applySufferChanges;

    public FightResult(Mind mind, bool shouldBeDestroyed, Action<Mind> applySufferChanges)
    {
        this.mind = mind;
        this.shouldBeDestroyed = shouldBeDestroyed;
        this.applySufferChanges = applySufferChanges;
    }
}