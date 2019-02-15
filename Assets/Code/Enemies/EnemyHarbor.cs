using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zeyro.Extensions;

public class EnemyHarbor : MonoBehaviour
{
    public GameObject ExplosionFX;
    public GameObject WreckagePrefab;

    public GameObject[] TurnToWreckage;

    public GameObject[] Assets;

    protected List<GameObject> _assets;

    void Awake()
    {
        _assets = Assets.ToList();
    }

    public void AssetDestroyed(GameObject asset)
    {
        _assets.Remove(asset);
        if(_assets.Count < 1)
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(Die_Cortoutine());    
    }

    protected IEnumerator Die_Cortoutine()
    {
        var children = new List<GameObject>();
        foreach(Transform child in gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        for(int i = 0; i < 10; i++)
        {
            var child = children.Choice().transform;
            var explosion = GameObject.Instantiate(ExplosionFX, child.position, Quaternion.identity);
            GameObject.Destroy(explosion, 1.0f);
            yield return new WaitForSeconds(.3f);
        }

        var wreckage = TurnToWreckage.ToList();
        foreach (Transform child in gameObject.transform)
        {
            var explosion = GameObject.Instantiate(ExplosionFX, child.position, Quaternion.identity);
            GameObject.Destroy(explosion, 1.0f);

            if (wreckage.Contains(child.gameObject))
            {
                var angle = UnityEngine.Random.Range(0.0f, 360.0f);
                var wreckageObject = GameObject.Instantiate(
                    WreckagePrefab, 
                    child.position, 
                    Quaternion.Euler(0.0f, 0.0f, angle)
                );
                var scale = wreckageObject.transform.localScale.x * UnityEngine.Random.Range(0.8f, 1.2f);
                wreckageObject.transform.localScale = Vector3.one * scale;
            }

            GameObject.Destroy(child.gameObject);
            yield return new WaitForSeconds(.3f);
        }

        GameObject.Destroy(gameObject);
    }
}
