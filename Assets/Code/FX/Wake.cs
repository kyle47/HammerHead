using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wake : MonoBehaviour
{
    public GameObject WakePrefab;

    public float TimeBetweenSpawns = .1f;
    public float LifeTime = 1.0f;
    public bool Active = true;

    private void Start()
    {
        StartCoroutine(WakeSpawn_Coroutine());
    }

    protected IEnumerator WakeSpawn_Coroutine()
    {
        while(true)
        {
            if(Active)
            {
                yield return new WaitForSeconds(TimeBetweenSpawns);
                var wakeObject = GameObject.Instantiate(WakePrefab, gameObject.transform.position, gameObject.transform.rotation);
                StartCoroutine(Wake_Coroutine(wakeObject));
            }
            yield return null;
        }

    }

    protected IEnumerator Wake_Coroutine(GameObject target)
    {
        var spriteRenderer = target.GetComponent<SpriteRenderer>();
        var endTime = Time.time + LifeTime;

        while(Time.time < endTime)
        {
            // Fade
            var color = spriteRenderer.color;
            color.a -= 1.0f * Time.deltaTime;
            spriteRenderer.color = color;

            // Expand
            target.transform.localScale += Vector3.one * Time.deltaTime;

            yield return null;
        }

        GameObject.Destroy(target);
    }


}
