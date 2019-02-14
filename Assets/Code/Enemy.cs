using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] Bits;

    public void Die()
    {
        Bits.ToList().ForEach(bit => {
            GameObject.Instantiate(bit, gameObject.transform.position, gameObject.transform.rotation);
        });
        GameObject.Destroy(gameObject);
    }
}
