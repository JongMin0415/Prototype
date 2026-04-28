using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineRotateShot : EnemyBaseShot
{
    public int bulletCount = 5;
    public float spacing = 0.5f;

    public override void Fire()
    {
        Vector2 dir = GetDirection();

        GameObject center = new GameObject("BulletGroup");
        center.transform.position = firePoint.position;

        RotatingBulletGroup group = center.AddComponent<RotatingBulletGroup>();
        group.Init(dir);

        Vector2 perpendicular = new Vector2(-dir.y, dir.x);

        for (int i = 0; i < bulletCount; i++)
        {
            float offset = (i - (bulletCount - 1) / 2f) * spacing;
            Vector3 localPos = perpendicular * offset;

            GameObject bullet = ObjectPool.Instance.GetEnemyBullet();

            if (bullet != null)
            {
                bullet.transform.SetParent(center.transform);
                bullet.transform.localPosition = localPos;
                bullet.transform.localRotation = Quaternion.identity;

                EnemyBullet b = bullet.GetComponent<EnemyBullet>();
                b.enabled = false; 
            }
        }
    }
}