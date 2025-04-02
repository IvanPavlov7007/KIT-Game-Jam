using System.Collections;
using UnityEngine;

public class AnimalMovement : MonoBehaviour, BaseMovement
{
    [SerializeField]
    Rigidbody2D rb;
    public float speed = 1f;

    Vector2 direction;
    public Vector2 Direction { get { return direction; } set { direction = value; } }

    protected void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected void FixedUpdate()
    {
        rb.velocity = Direction * speed;
    }

}