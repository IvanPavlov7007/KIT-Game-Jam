using System.Collections;
using UnityEngine;

public class AnimalCollision : MonoBehaviour
{
    FlockingMember _tA;
    FlockingMember thisFlockingMember { get { if (_tA == null) _tA = GetComponentInParent<FlockingMember>(); return _tA; } }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherAI = collision.gameObject.GetComponentInParent<FlockingMember>();

        thisFlockingMember.CollidedWithAnother(otherAI);
    }
}