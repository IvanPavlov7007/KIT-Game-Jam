using System.Collections;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    AnimalMovement animalMovement;
    public AnimalMovement AnimalMovement { get { return animalMovement; } }
    [SerializeField]
    float flockPushVelocity;

    public float aiTickProbability = 0.3f;
    public float perceptionDistance = 1f;// TODO create a grid or something to speed up
    public float separationDistance = 0.3f;
    public float separatingDistanceToVelocityCompensation = 0.3f;
    public float goalVelocityAmount = 0.5f;
    [HideInInspector]
    public Flock flock;

    protected void Update()
    {
        if (flock != null)
        {
            if(Random.value <= aiTickProbability)
            {
                moveInFlock();
            }
        }
    }

    protected void moveInFlock()
    {
        Vector2 floatCenter = flock.position;
        Vector2 distance = Vector2.zero;

        Vector2 avgVel = Vector2.zero;
        Vector2 sumSeparatingDistances = Vector2.zero;

        float speed;
        foreach (var ai in flock.animalsInFlock)
        {
            distance = transform.position - ai.transform.position;
            if(distance.magnitude <= perceptionDistance)
            {
                if(distance.magnitude <= separationDistance)
                {
                    sumSeparatingDistances += distance;
                }
            }
        }

        Vector2 goalDistance = flock.position - (Vector2)transform.position;

        animalMovement.TargetVelocity = flock.velocity + goalDistance * goalVelocityAmount * Time.deltaTime + sumSeparatingDistances * Time.deltaTime * separatingDistanceToVelocityCompensation;
    }

    public virtual void CollidedWithAnother(AnimalAI other)
    {
        if (flock != null)
        {
            flock.handleCollision(this,other);
        }
        else if (other.flock == null)
        {
            GameManager.Instance.buildFlock(this, other);
        }
    }
}