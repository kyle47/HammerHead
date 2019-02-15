using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerGenerator : Enemy
{
    public GameObject ExplosionFX;
    public EnemyHarbor Harbor;

    public override void Die()
    {
        if(Harbor)
        {
            Harbor.AssetDestroyed(gameObject);
        }

        GameObject.Destroy(gameObject);
        var crackEffect = GameObject.Instantiate(ExplosionFX, gameObject.transform.position, gameObject.transform.rotation);
        GameObject.Destroy(crackEffect, 1.0f);
    }
}
