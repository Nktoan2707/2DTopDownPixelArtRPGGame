using Assets.Interfaces;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private float damage = 1f;
    private float knockbackForce = 10f;

    private Vector2 rightAttackOffset;
    public Collider2D swordCollider;
    public Vector3 faceRight = new(0.12f, -0.06f, 0);
    public Vector3 faceLeft = new(-0.12f, -0.06f, 0);

    // Start is called before the first frame update
    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword collider not set");
        }

        rightAttackOffset = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    //{
    //    print("hit1");
    //    collision.SendMessage("OnHit", damage);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.TryGetComponent<IDamageable>(out var damagableObject))
        {
            return;
        }

        Vector3 parentPosition = transform.parent.position;
        Vector2 direction = (Vector2)(collision.gameObject.transform.position - parentPosition).normalized;
        Vector2 knockback = direction * knockbackForce;

        damagableObject.OnHit(damage, knockback);
    }

    public void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}