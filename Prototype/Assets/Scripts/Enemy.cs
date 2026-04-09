using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 2f;
    protected Rigidbody2D rb;
    protected Transform target;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    protected void Move(Vector2 dir)
    {
        rb.velocity = dir * moveSpeed;
    }
}
