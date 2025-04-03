using System.Collections;
using UnityEngine;

public class AnimalCollision : MonoBehaviour
{
    AnimalAI _tA;
    AnimalAI thisAI { get { if (_tA == null) _tA = GetComponentInParent<AnimalAI>(); return _tA; } }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherAI = collision.gameObject.GetComponentInParent<AnimalAI>();

        thisAI.CollidedWithAnother(otherAI);
    }
}