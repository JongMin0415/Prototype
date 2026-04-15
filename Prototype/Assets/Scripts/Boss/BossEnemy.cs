using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public BulletSpawner spawner;

    public float patternDelay = 1.5f;

    private bool phase2 = false;

    protected override void Start()
    {
        base.Start();

        if (rb != null)
            rb.velocity = Vector2.zero;

        StartCoroutine(BossLoop());
    }

    void Update()
    {
        if (!phase2 && currentHP <= maxHP * 0.5f)
        {
            phase2 = true;
        }
    }


    IEnumerator BossLoop()
    {
        while (true)
        {
            if (!phase2)
            {
                yield return StartCoroutine(Pattern_PredictShot());
                yield return new WaitForSeconds(patternDelay);

                yield return StartCoroutine(Pattern_Fan());
                yield return new WaitForSeconds(patternDelay);

                yield return StartCoroutine(Pattern_Burst());
                yield return new WaitForSeconds(patternDelay);

                yield return StartCoroutine(Pattern_Circle());
                yield return new WaitForSeconds(patternDelay);
            }
            else
            {
                yield return StartCoroutine(Pattern_Spin());
                yield return new WaitForSeconds(patternDelay);

                yield return StartCoroutine(Pattern_Circle_And_Burst());
                yield return new WaitForSeconds(patternDelay);

                yield return StartCoroutine(Pattern_Burst());
                yield return new WaitForSeconds(patternDelay);
            }
        }
    }
    Vector2 PredictTargetPosition(float bulletSpeed)
    {
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();

        if (targetRb == null)
            return target.position;

        Vector2 toTarget = target.position - transform.position;
        float distance = toTarget.magnitude;

        float time = distance / bulletSpeed;

        Vector2 futurePos = (Vector2)target.position + targetRb.velocity * time;

        return futurePos;
    }

    IEnumerator Pattern_Circle()
    {
        int bulletCount = 50;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * Mathf.PI * 2 / bulletCount;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            spawner.SpawnBullet(dir);
        }

        yield return new WaitForSeconds(1f);
    }
    IEnumerator Pattern_Burst()
    {
        int shotCount = 20;
        float delay = 0.1f;

        for (int i = 0; i < shotCount; i++)
        {
            Vector2 dir = (target.position - transform.position).normalized;

            float spread = Random.Range(-5f, 5f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spread;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 finalDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            spawner.SpawnBullet(finalDir);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Pattern_Spin()
    {
        float duration = 5f;
        float timer = 0f;
        float angle = 0f;

        while (timer < duration)
        {
            int bulletCount = 12;

            for (int i = 0; i < bulletCount; i++)
            {
                float a = angle + i * Mathf.PI * 2 / bulletCount;
                Vector2 dir = new Vector2(Mathf.Cos(a), Mathf.Sin(a));

                spawner.SpawnBullet(dir);
            }

            angle += 0.2f;
            timer += 0.2f;

            yield return new WaitForSeconds(0.2f);
        }
        yield return StartCoroutine(Pattern_Circle());
    }

    IEnumerator Pattern_Fan()
    {
        int bulletCount = 10;
        float spread = 45f;

        Vector2 baseDir = (target.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = baseAngle - spread / 2 + spread * i / (bulletCount - 1);
            float rad = angle * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            spawner.SpawnBullet(dir);
        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator Pattern_PredictShot()
    {
        int shotCount = 20;
        float delay = 0.1f;

        float bulletSpeed = 8f;

        for (int i = 0; i < shotCount; i++)
        {
            Vector2 predictedPos = PredictTargetPosition(bulletSpeed);
            Vector2 dir = (predictedPos - (Vector2)transform.position).normalized;

            spawner.SpawnBullet(dir);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Pattern_Circle_And_Burst()
    {
        StartCoroutine(Pattern_Circle());
        yield return StartCoroutine(Pattern_Burst());
    }
}