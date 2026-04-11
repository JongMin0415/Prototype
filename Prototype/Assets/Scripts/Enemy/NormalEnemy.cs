using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NormalEnemy : Enemy
{
    public float attackRange = 5f;
    public float attackCooldown = 1.5f;

    public EnemyBaseShot shot;

    private float lastAttackTime;

    void FixedUpdate()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            Move(dir);
        }
        else
        {
            rb.velocity = Vector2.zero;

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (shot != null)
                    shot.Fire();

                lastAttackTime = Time.time;
            }
        }
    }
}