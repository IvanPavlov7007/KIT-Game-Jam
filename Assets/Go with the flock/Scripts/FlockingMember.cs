using System.Collections;
using UnityEngine;

public class FlockingMember : MonoBehaviour
{
    [SerializeField]
    AnimalBase animalBase;
    public AnimalBase AnimalBase { get { return animalBase; } }
    [SerializeField]
    float flockPushVelocity;

    public float aiTickProbability = 0.3f;
    public float perceptionDistance = 1f;
    public float separationDistance = 0.3f;
    public float separatingDistanceToVelocityCompensation = 0.3f;
    public float goalVelocityAmount = 0.5f;
    [HideInInspector]
    public Flock flock;
    public Mind Mind { get; protected set; }

    public StatsEntity stats = new StatsEntity
    {
        attack = 1f,
        additionalAttack = 1f,
        health = 1f,
        maxHealth = 1f,
        additionalHealth = 1f,
        speed = 1f,
        additionalSpeed = 1f,
        view = 1f,
        additionalView = 1f
    };

    private void Start()
    {
        if (Mind == null)
            GameManager.Instance.provideAnyMind(this);
    }

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

        animalBase.TargetVelocity = flock.velocity + goalDistance * goalVelocityAmount * Time.deltaTime + sumSeparatingDistances * Time.deltaTime * separatingDistanceToVelocityCompensation;
    }

    public virtual void CollidedWithAnother(FlockingMember other)
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