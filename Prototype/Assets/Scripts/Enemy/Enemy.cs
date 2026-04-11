using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float maxHP = 10f;
    protected float currentHP;

    protected Rigidbody2D rb;
    protected Transform target;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        currentHP = maxHP;
    }

    protected void Move(Vector2 dir)
    {
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
    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;

        Debug.Log($"{gameObject.name} HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
