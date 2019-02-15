using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrigate : Enemy
{
    public DeathSpawner DeathSpawner;
    public GameObject CrackFXPrefab;

    public override void Die()
    {
        GameObject.Instantiate(DeathSpawner, gameObject.transform.position, gameObject.transform.rotation);
        GameObject.Destroy(gameObject);

        var crackEffect = GameObject.Instantiate(CrackFXPrefab, gameObject.transform.position, gameObject.transform.rotation);
        GameObject.Destroy(crackEffect, .1f);
    }
}
