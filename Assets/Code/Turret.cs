using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Target;
    public float RotationSpeed = 10.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void Update()
    {
        if(Target == null)
        {
            return;
        }

        var vectorToTarget = Target.transform.position - transform.position;
        var angle = -Mathf.Atan2(vectorToTarget.x, vectorToTarget.y) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);
    }

}
