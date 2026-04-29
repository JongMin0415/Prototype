using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    public float dashSpeed = 8f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 2f;

    private bool isDashing = false;
    private float lastDashTime;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (target == null) return;

        if (!isDashing)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            Move((target.position - transform.position).normalized);

            if (distance < 5f && Time.time > lastDashTime + dashCooldown)
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        Vector2 dashDir = (target.position - transform.position).normalized;

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);

        float timer = 0f;
        while (timer < dashDuration)
        {
            rb.velocity = dashDir * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;

        isDashing = false;
    }
}