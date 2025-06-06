﻿using System.Collections;
using UnityEngine;

public class AnimalCollision : MonoBehaviour
{
    FlockingMember _tA;
    FlockingMember thisFlockingMember { get { if (_tA == null) _tA = GetComponentInParent<FlockingMember>(); return _tA; } }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherFlockingMember = collision.gameObject.GetComponentInParent<FlockingMember>();
        
        if(otherFlockingMember == null)
        {
            Debug.LogWarning(otherFlockingMember.name + " is not an animal");
            return;
        }    

        if(thisFlockingMember.Mind == null)
        {
            Debug.LogWarning(thisFlockingMember.name + " doesn't have a mind");
        }

        thisFlockingMember.Mind.reportCollision(otherFlockingMember);
    }
}