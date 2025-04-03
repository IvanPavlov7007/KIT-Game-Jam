using System.Collections;
using UnityEngine;

public class AnimalMovement : MonoBehaviour, BaseMovement
{
    [SerializeField]
    Rigidbody2D rb;
    public float speed = 1f;

    Vector2 direction;
    public virtual Vector2 Direction { get { return TargetVelocity.normalized; } set { direction = value.normalized; TargetVelocity = direction * speed; } }
    public virtual Vector2 TargetVelocity { get; set; }
    protected void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected void FixedUpdate()
    {
        rb.velocity = TargetVelocity * speed;
    }

}