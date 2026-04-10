using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleShot : EnemyBaseShot
{
    public int bulletCount = 12;

    public override void Fire()
    {
        float step = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = step * i;

            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            SpawnBullet(dir.normalized);
        }
    }
}