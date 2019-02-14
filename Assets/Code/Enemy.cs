using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DeathSpawner DeathSpawner;

    public void Die()
    {
        GameObject.Instantiate(DeathSpawner, gameObject.transform.position, gameObject.transform.rotation);
        GameObject.Destroy(gameObject);
    }
}
