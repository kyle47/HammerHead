using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunPlatform : Enemy
{
    public GameObject ExplosionFX;

    public override void Die()
    {
        GameObject.Destroy(gameObject);
        var crackEffect = GameObject.Instantiate(ExplosionFX, gameObject.transform.position, gameObject.transform.rotation);
        GameObject.Destroy(crackEffect, 1.0f);
    }
}
