using System.Collections;
using UnityEngine;

public class AnimalBase : MonoBehaviour, BaseMovement
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    FlockingMember flockingMember;
    public FlockingMember FlockingMember { get => flockingMember; }

    Vector2 direction;
    public virtual Vector2 Direction 
    { 
        get { return TargetVelocity.normalized; } 
        set { direction = value.normalized; 
            TargetVelocity = direction * overallSpeed; } 
    }

    public virtual float overallSpeed { get { return flockingMember.stats.speed + flockingMember.stats.additionalSpeed; } }
    public virtual Vector2 TargetVelocity { get; set; }

    protected void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected void FixedUpdate()
    {
        rb.velocity = TargetVelocity * overallSpeed;
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

}