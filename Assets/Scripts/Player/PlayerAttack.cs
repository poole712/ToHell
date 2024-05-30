using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public float MaxChargeTime = 3f;
    public ParticleSystem slamParticle;
    public LayerMask HitLayerMask;
    public LayerMask AttackLayerMask;

    public GameObject segmentManager;
    public float MaxDamage = 15f;
    public float ChargeSpeed = 0.05f;
    public Image PowerBar;

    private Animator _animator;
    private bool _isCharging;
    private float _power;
    private bool _chargeInputDown;
    private Coroutine _chargingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the GameObject
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse input for editor or desktop
        if (Input.GetMouseButtonDown(0))
        {
            if (_chargingCoroutine != null)
            {
                StopCoroutine(_chargingCoroutine);
            }
            _chargingCoroutine = StartCoroutine(CheckIfStillHolding());
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndCharging();
        }

//Precautions to ensure these are only run when playing on a mobile device
#if UNITY_ANDROID || UNITY_IOS
        // Touch input for mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (_chargingCoroutine != null)
                {
                    StopCoroutine(_chargingCoroutine);
                }
                _chargingCoroutine = StartCoroutine(CheckIfStillHolding());
            }
            if (touch.phase == TouchPhase.Ended)
            {
                EndCharging();
            }
        }
#endif
    }

    private IEnumerator CheckIfStillHolding()
    {
        _chargeInputDown = true;
        yield return new WaitForSeconds(0.2f);

        if (_chargeInputDown)
        {
            StartCharging();
        }
    }

    private void EndCharging()
    {
        _chargeInputDown = false;

        if (_isCharging)
        {
            _isCharging = false;
            _animator.SetBool("Holding", false);
        }

        if (_chargingCoroutine != null)
        {
            StopCoroutine(_chargingCoroutine);
            _chargingCoroutine = null;
        }
    }

    private void StartCharging()
    {
        if (!_isCharging)
        {
            _isCharging = true;
            _animator.SetBool("Holding", true);
            _power = 0f; // Reset power when starting a new charge
            StartCoroutine(ChargeRoutine());
        }
    }

    private IEnumerator ChargeRoutine()
    {
        while (_isCharging && _power < MaxDamage)
        {
            _power += ChargeSpeed;
            PowerBar.fillAmount = _power / MaxDamage;
            yield return null; // Wait until the next frame
        }
    }

    //Called in the Attack animation event!
    public void Attack()
    {
        //Ray for shooting out ahead of player where the hammer would hit
        RaycastHit2D rayHit = Physics2D.Raycast(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z),
        new Vector2(Vector2.down.x + 1, Vector2.down.y), 0.1f, HitLayerMask);

        //Ray to check for enemy hits
        RaycastHit2D sphereHit = Physics2D.CircleCast(new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z),
        1f, new Vector2(Vector2.down.x + 5, Vector2.down.y), 0, AttackLayerMask);

        //Just for debugging where the ray is going in the editor
        Debug.DrawRay(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z),
        new Vector2(Vector2.down.x + 1, Vector2.down.y * 0.1f), Color.green, 3f);

        //Check if enemy hit before checking if layer been hit as player can only hit layer OR enemy not both.
        if (sphereHit.collider != null && sphereHit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Destroy(sphereHit.collider.gameObject);
        }
        if(sphereHit.collider != null && sphereHit.collider.CompareTag("Fireball"))
        {
            sphereHit.collider.GetComponent<Fireball>().FlipBall();
        }
        else if (rayHit.collider != null && !rayHit.collider.CompareTag("Layer 5 (Bottom)"))
        {
            Instantiate(slamParticle, rayHit.point, Quaternion.identity);
            segmentManager.GetComponent<SegmentManager>().DamageLayer(_power);
            Camera.main.GetComponent<S_SimpleCamera>().Shake();
        }

        _power = 0;
        PowerBar.fillAmount = _power / MaxDamage;
    }

    // Speed boost methods
    public void IncreaseSpeed(float amount, float duration)
    {
        StartCoroutine(SpeedBoost(amount, duration));
    }

    private IEnumerator SpeedBoost(float amount, float duration)
    {
        ChargeSpeed += amount;
        yield return new WaitForSeconds(duration);
        ChargeSpeed = 0.05f;
    }
}
