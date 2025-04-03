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

    public StatsCollection stats = new StatsCollection
    {
        attack = 1,
        additionalAttack = 1,
        health = 1,
        maxHealth = 1,
        additionalHealth = 1,
        speed = 1,
        additionalSpeed = 1,
        view = 1,
        additionalView = 1
    };

    private void Start()
    {
        if (Mind == null)
            Mind = GameManager.Instance.provideMind(this);
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
}