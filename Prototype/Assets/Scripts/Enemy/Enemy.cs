using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float maxHP = 10f;
    public float knockbackForce = 5f;
    protected float currentHP;

    protected Rigidbody2D rb;
    protected Transform target;
    protected bool isKnockedBack = false;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        currentHP = maxHP;
    }

    protected void Move(Vector2 dir)
    {
        if (isKnockedBack) return;

        rb.velocity = dir * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
    public virtual void TakeDamage(float damage, Vector2 hitDirection)
    {
        currentHP -= damage;
        StartCoroutine(Knockback(hitDirection));

        if (currentHP <= 0)
        {
            Die();
        }
    }
    IEnumerator Knockback(Vector2 dir)
    {
        isKnockedBack = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        isKnockedBack = false;
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
