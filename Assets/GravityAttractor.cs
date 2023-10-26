using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -9.81f;
    
    public void Attract(Transform body)
    {
       // Direction from GravityBody to center of planet
       Vector3 targetDir = (body.position - gameObject.transform.position).normalized;
       Vector3 bodyUp = body.up;

       // Applies direction from current to target
       body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
       body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
    }
    
}
