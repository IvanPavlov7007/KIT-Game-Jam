using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Flock : MonoBehaviour, BaseMovement
{
    public Vector2 Direction { get { return velocity.normalized; } set { velocity = value.normalized * speed; } }
    public float speed { get; private set; } = 1f;

    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }

    public List<AnimalAI> animalsInFlock { get; private set; } = new List<AnimalAI>();

    protected virtual void FixedUpdate()
    {
        position = velocity * Time.fixedDeltaTime;
    }

    public virtual void assignInitialPosition(Vector2 pos)
    {
        position = pos;
    }

    public virtual void createFlock(ICollection<AnimalAI> animalAIs)
    {
        animalsInFlock.AddRange(animalAIs);
        foreach (var an in animalAIs)
        {
            an.flock = this;
        }
    }

    void addFlockMember(AnimalAI ai)
    {
        ai.flock = this;
        animalsInFlock.Add(ai);
    }

    //Be careful, since collision is being called twice from both sides!
    public void handleCollision(AnimalAI flocksMember, AnimalAI another)
    {
        if(another.flock == null)
        {
            addFlockMember(another);
        }
        //TODO handle else, maybe thorugh Game Manager
    }

}