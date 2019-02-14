using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1.0f;
    public float MaxSpeed = 5.0f;
    public float TurnSpeed = 1.0f;

    public float ChargeSpeed = 10.0f;
    public float ChargeTime = 1.0f;

    public Rigidbody2D Rigidbody2D;

    protected float _inputVertical;
    protected float _inputHorizontal;

    protected bool _blockInput;

    public Animator Animator;
    public CameraController CameraController;
    public Transform RaycastPoint;

    public GameObject HitEffectPrefab;

    private void Update()
    {
        if(_blockInput)
        {
            return;
        }

        // Move forward
        _inputVertical = Mathf.Max(Input.GetAxis("Vertical"), -0.5f);
        Animator.SetBool("Swimming", _inputVertical != 0.0f);

        var direction = gameObject.transform.up * _inputVertical;
        Rigidbody2D.AddForce(direction);

        Rigidbody2D.drag = _inputHorizontal == 0.0f ? 0.4f: 0.8f;

        // Turn
        _inputHorizontal = Input.GetAxis("Horizontal");
        var torque = -_inputHorizontal * TurnSpeed;
        Rigidbody2D.angularVelocity = torque;

        Animator.SetBool("TurnRight", _inputHorizontal > 0.0f);
        Animator.SetBool("TurnLeft", _inputHorizontal < 0.0f);
        Animator.SetBool("Turning", _inputHorizontal != 0.0f);

        // Throttle speed
        var goingTooFast = Rigidbody2D.velocity.magnitude > MaxSpeed;
        if(goingTooFast)
        {
            Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * MaxSpeed;
        }

        // Charge
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Don't allow the player to charge into non enemies if they are too close
            var hit = Physics2D.Raycast(RaycastPoint.position, RaycastPoint.up, 2.0f);
            var canCharge = hit.transform == null || hit.transform.name != "Island";

            if(canCharge)
            {
                Charge();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.name == "Island")
        {
            CameraController.ScreenShake();
        }
    }

    public void Charge()
    {
        StartCoroutine(Charge_Coroutine());
    }

    protected IEnumerator Charge_Coroutine()
    {
        Animator.SetBool("Charging", true);

        _blockInput = true;
        Rigidbody2D.angularVelocity = 0.0f;
        Rigidbody2D.freezeRotation = true;
        CameraController.CameraSize = 4.0f;
        CameraController.LeadDistance = 4.0f;

        var initialDrag = Rigidbody2D.drag;
        Rigidbody2D.drag = 0.0f;

        var chargeDirection = gameObject.transform.up;

        var initialVelocity = Rigidbody2D.velocity;
        Rigidbody2D.velocity = chargeDirection * ChargeSpeed;

        var chargeEndTime = Time.time + ChargeTime;
        while(Time.time < chargeEndTime)
        {
            var hit = Physics2D.Raycast(RaycastPoint.position, gameObject.transform.up, 0.2f);
            if(hit.transform)
            {

                var enemy = hit.transform.GetComponent<Enemy>();
                StartCoroutine(SlowTime_Coroutine());
                if(enemy)
                {
                    var effect = GameObject.Instantiate(HitEffectPrefab, enemy.transform.position, Quaternion.identity);
                    GameObject.Destroy(effect, 1.0f);
                    enemy.Die();
                }
            }

            yield return null;
        }

        CameraController.LeadDistance = 2.0f;
        CameraController.CameraSize = 4.5f;
        Rigidbody2D.freezeRotation = false;
        Rigidbody2D.drag = initialDrag;
        Rigidbody2D.velocity = chargeDirection * initialVelocity.magnitude;
        _blockInput = false;

        Animator.SetBool("Charging", false);
    }

    protected IEnumerator SlowTime_Coroutine()
    {
        Time.timeScale = .2f;
        yield return new WaitForSeconds(.025f);
        Time.timeScale = 1.0f;
    }
}
