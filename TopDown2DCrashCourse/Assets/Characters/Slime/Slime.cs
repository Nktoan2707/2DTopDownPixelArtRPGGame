using Assets.Interfaces;
using System.Linq;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private float knockbackForce = 10f;

    private float damage = 1f;

    public DetectionZone detectionZone;

    private float moveSpeed = 700f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Collider2D detectedObject;
        if (detectionZone.detectedObjects.Count == 0)
        {
            return;
        }

        detectedObject = detectionZone.detectedObjects.First();
        Vector2 direction = (detectedObject.transform.position - transform.position).normalized;

        rb.AddForce(direction * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!collider.collider.TryGetComponent<IDamageable>(out var damagableObject))
        {
            return;
        }

        Vector2 direction = (Vector2)(collider.transform.position - transform.position).normalized;
        Vector2 knockback = direction * knockbackForce;
        damagableObject.OnHit(damage, knockback);
    }
}