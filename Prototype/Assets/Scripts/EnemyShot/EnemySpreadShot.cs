using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpreadShot : EnemyBaseShot
{
    public int bulletCount = 5;
    public float spreadAngle = 45f;

    public override void Fire()
    {
        Vector2 baseDir = GetDirection();
        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - spreadAngle / 2;
        float step = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + step * i;

            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            SpawnBullet(dir.normalized);
        }
    }
}