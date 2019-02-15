using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 1.0f;
    public Rigidbody2D Rigidbody2D;

    public void Initialize(Vector3 direction)
    {
        Rigidbody2D.velocity = direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if(player)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
