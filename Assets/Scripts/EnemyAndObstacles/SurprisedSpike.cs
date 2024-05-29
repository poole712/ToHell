using UnityEngine;
using System.Collections;

public class SurprisedSpike : GameEntities
{
    public float riseSpeed = 2f;
    public float activeDuration = 2f;
    public float cooldownDuration = 3f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isActive = false;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * transform.localScale.y;
        StartCoroutine(ActivateSpike());
    }

    IEnumerator ActivateSpike()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownDuration);
            isActive = true;
            while (transform.position.y < targetPosition.y)
            {
                transform.position += Vector3.up * riseSpeed * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(activeDuration);
            while (transform.position.y > initialPosition.y)
            {
                transform.position -= Vector3.up * riseSpeed * Time.deltaTime;
                yield return null;
            }
            isActive = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(20f); // Adjust damage as needed
            }
        }
    }
}
