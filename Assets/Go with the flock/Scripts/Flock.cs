using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

public class Flock : MonoBehaviour, BaseMovement
{
    public Vector2 Direction { get; set; }
    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }

    public StatsCollection stats;

    public List<FlockingMember> animalsInFlock { get; private set; } = new List<FlockingMember>();

    protected virtual void FixedUpdate()
    {
        velocity = Direction.normalized * (stats.additionalSpeed + 1);
        position += velocity * Time.fixedDeltaTime;
    }


    public void addFlockMembers(IEnumerable<FlockingMember> members)
    {
        foreach(var memb in members)
        {
            addStats(memb.stats);
            memb.flock = this;
        }
        animalsInFlock.AddRange(members);
    }

    public void initializeFlock(FlockingMember flockingMember)
    {
        addStats(flockingMember.stats);
        flockingMember.flock = this;
        animalsInFlock.Add(flockingMember);
        position = flockingMember.transform.position;
    }

    public void removeFlockingMemberStats(FlockingMember member)
    {
        stats.additionalAttack -= member.stats.additionalAttack;
        stats.additionalHealth -= member.stats.additionalHealth;
        stats.additionalSpeed -= member.stats.additionalSpeed;
        stats.additionalView -= member.stats.additionalView;
        stats.health -= member.stats.health;
    }

    internal void kill()
    {
        foreach(var mem in animalsInFlock)
        {
            mem.AnimalBase.Kill();
        }
    }

    public void addStats(StatsCollection additional)
    {
        stats.additionalAttack += additional.additionalAttack;
        stats.additionalHealth += additional.additionalHealth;
        stats.additionalSpeed += additional.additionalSpeed;
        stats.additionalView += additional.additionalView;
        stats.health += additional.health;
    }

    public void DistributeDamage(int damage)
    {
        stats.health -= damage;
        // Sort by highest health first to minimize deaths
        var animals = new List<FlockingMember>(animalsInFlock).OrderByDescending(e => e.stats.health).ToList();

        foreach (var member in animals)
        {
            if (damage <= 0) break;

            int damageToApply = Mathf.Min(member.stats.health - 1, damage); // Ensure it doesn't go to zero if possible
            member.stats.health -= damageToApply;
            damage -= damageToApply;
        }

        // If there is still remaining damage, apply it, possibly killing
        foreach (var member in animals)
        {
            if (damage <= 0) break;

            int damageToApply = Mathf.Min(member.stats.health, damage);
            member.stats.health -= damageToApply;
            if (member.stats.health == 0)
            {
                KillOne(member);
            }
            damage -= damageToApply;
        }
    }

    public void KillOne(FlockingMember member)
    {
        removeFlockingMemberStats(member);
        animalsInFlock.Remove(member);
        member.AnimalBase.Kill();
    }

}
[CustomEditor(typeof(Flock))]
public class FlockEditor : Editor
{
    private void OnSceneGUI()
    {
        Vector3 pos = (target as Flock).position;
        Handles.DrawWireCube(pos, Vector3.one * 0.5f);
    }
}