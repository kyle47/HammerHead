using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    protected const float TARGET_CHECK_TIME = .5f;

    public GameObject BulletPrefab;
    public GameObject Target;
    public Transform BulletSpawn;
    public float RotationSpeed = 10.0f;

    public float SightRange = 5.0f;
    public float Reload = 2.0f;

    private void Start()
    {
        StartCoroutine(CheckForTargets_Coroutine());
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

    protected IEnumerator CheckForTargets_Coroutine()
    {
        var nextFireTime = Time.time + Reload;
        
        while (true)
        {
            if (Target)
            {
                if(Time.time > nextFireTime)
                {
                    var bulletObject  = GameObject.Instantiate(BulletPrefab, BulletSpawn.position, gameObject.transform.rotation);
                    var bullet = bulletObject.GetComponent<Bullet>();
                    bullet.Initialize(gameObject.transform.up);
                    nextFireTime = Time.time + Reload;
                }

                if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > SightRange)
                {
                    Target = null;
                }
            }
            else
            {
                var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, SightRange);
                var player = colliders.ToList().Find(collider => collider.gameObject.GetComponent<PlayerController>());
                if(player)
                {
                    Target = player.gameObject;
                    nextFireTime = Time.time + Reload;
                }
            }
            yield return new WaitForSeconds(TARGET_CHECK_TIME);
        }
    }

}
