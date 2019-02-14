using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpawner : MonoBehaviour
{
    [Serializable]
    public class DeathSpawnInfo
    {
        public GameObject Debris;
        public float Velocity;
    }

    public DeathSpawnInfo[] Info;

    private void Start()
    {
        Info.ToList().ForEach(info =>
        {
            var rigidBody2D = info.Debris.GetComponent<Rigidbody2D>();
            rigidBody2D.velocity = info.Debris.transform.forward * info.Velocity;
            info.Debris.transform.parent = null;
        });

        GameObject.Destroy(gameObject);
    }
}
