using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float TrackingDistance = 0.5f;
    public float LeadDistance = 2.0f;

    public Rigidbody2D Rigidbody2D;
    public Camera Camera;

    public Transform Target;

    public float CameraSize = 4.5f;

    private void Update()
    {
        Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, CameraSize, .05f);

        var targetPosition = (Target.transform.up * LeadDistance) + Target.transform.position;

        var distance = Vector2.Distance(targetPosition, gameObject.transform.position);
        if(distance < TrackingDistance)
        {
            return;
        }

        var direction = (targetPosition - gameObject.transform.position);

        Rigidbody2D.velocity = direction;
    }

    public void ScreenShake()
    {
        StartCoroutine(ScreenSchake_Coroutine());
    }

    protected IEnumerator ScreenSchake_Coroutine()
    {
        var orignalPosition = gameObject.transform.localPosition;
        var endTime = Time.time + .1f;
        while(Time.time < endTime)
        {
            var offset = (Vector3)Random.insideUnitCircle * 0.1f;
            gameObject.transform.localPosition = orignalPosition + offset;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.transform.position = orignalPosition;
    }
}
