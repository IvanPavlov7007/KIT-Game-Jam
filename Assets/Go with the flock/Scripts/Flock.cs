using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Flock : MonoBehaviour, BaseMovement
{
    public Vector2 Direction { get { return velocity.normalized; } set { velocity = value.normalized * speed; } }
    public float speed { get; private set; } = 1f;

    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }

    public StatsEntity stats;

    public List<FlockingMember> animalsInFlock { get; private set; } = new List<FlockingMember>();

    protected virtual void FixedUpdate()
    {
        position = velocity * Time.fixedDeltaTime;
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

    void addStats(StatsEntity additional)
    {
        stats += additional;

    }

}