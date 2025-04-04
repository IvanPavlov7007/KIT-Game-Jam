using System.Collections;
using UnityEngine;

public class AnimalBase : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    FlockingMember flockingMember;
    [SerializeField]
    bool isAntelope;
    [SerializeField]
    Animator anim;
    [SerializeField]
    float idleTolerance = 0.2f;
    [SerializeField]
    float randomDeviationScale;

    public FlockingMember FlockingMember { get => flockingMember; }

    public virtual Vector2 TargetVelocity { get; set; }

    protected void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetFloat("offset", Random.value);
        anim.SetFloat("speedDeviation", Random.Range(1f - randomDeviationScale, 1f + randomDeviationScale));

        if(Random.value > 0.5f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void chreckFlip()
    {
        if (TargetVelocity.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    protected virtual void Update()
    {
        if (TargetVelocity.magnitude >= idleTolerance)
        {
            if (isAntelope)
            {
                anim.SetBool("amble", true);
            }
            else
                anim.SetBool("walk", true);
            chreckFlip();
        }
        else
        {
            anim.SetBool("amble", false);
            anim.SetBool("walk", false);
        }


    }

    protected void FixedUpdate()
    {
        rb.velocity = TargetVelocity;
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

}