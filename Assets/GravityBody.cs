using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    private GravityAttractor planet;
    
    private void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        // rotation is frozen as its handled by the GravityAttractor
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        // All gravitybodies do NOT collide. (prevents getting stuck in "corners")
        Physics.IgnoreLayerCollision(6,8);
    }

    private void FixedUpdate()
    {
        // pass the gravitybody to the plane to apply gravity
        planet.Attract(transform);
    }
}
