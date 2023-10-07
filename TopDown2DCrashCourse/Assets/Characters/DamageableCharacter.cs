using Assets.Interfaces;
using UnityEngine;

namespace Assets.Characters
{
    public class DamageableCharacter : MonoBehaviour, IDamageable
    {
        public bool canTurnInvincible = false;
        public HealthText healthText;
        private float _health = 4;
        private bool _invincible = false;
        private bool _targetable = true;
        private Animator animator;
        private float invincibilityTime = 0.5f;
        private float invincibleTimeElapsed = 0f;
        private Collider2D physicsCollider;
        private Rigidbody2D rb;

        public float Health
        {
            set
            {
                if (value < _health)
                {
                    animator.SetTrigger("hit");
                    RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                    textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                    Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                    textTransform.SetParent(canvas.transform);
                }

                _health = value;

                if (_health <= 0)
                {
                    animator.SetBool("isAlive", false);
                    Targetable = false;
                }
            }

            get { return _health; }
        }

        public bool Invincible
        {
            get => _invincible; set
            {
                _invincible = value;
                if (_invincible == true)
                {
                    invincibleTimeElapsed = 0f;
                }
            }
        }

        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        public bool Targetable
        {
            get { return _targetable; }
            set
            {
                _targetable = value;

                rb.simulated = value;

                physicsCollider.enabled = value;
            }
        }

        public void FixedUpdate()
        {
            if (Invincible && canTurnInvincible)
            {
                invincibleTimeElapsed += Time.deltaTime;
                if (invincibleTimeElapsed > invincibilityTime)
                {
                    Invincible = false;
                    invincibleTimeElapsed = 0f;
                }
            }
        }

        public void OnHit(float damage, Vector2 knockback)
        {
            if (Invincible)
            {
                return;
            }

            Health -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (canTurnInvincible)
            {
                Invincible = true;
            }
        }

        public void OnHit(float damage)
        {
            if (Invincible)
            {
                return;
            }

            Health -= damage;

            if (canTurnInvincible)
            {
                Invincible = true;
            }
        }

        public void OnObjectDestroy()
        {
            Destroy(gameObject);
        }

        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            physicsCollider = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            animator.SetBool("isAlive", true);
        }
    }
}