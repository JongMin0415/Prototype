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

        //  중심 오브젝트 생성
        GameObject center = new GameObject("BulletGroup");
        center.transform.position = firePoint.position;

        RotatingBulletGroup group = center.AddComponent<RotatingBulletGroup>();
        group.Init(dir);

        // 수직 방향 (일렬 배치용)
        Vector2 perpendicular = new Vector2(-dir.y, dir.x);

        for (int i = 0; i < bulletCount; i++)
        {
            float offset = (i - (bulletCount - 1) / 2f) * spacing;

            Vector3 localPos = perpendicular * offset;

            GameObject bullet = Instantiate(bulletPrefab, center.transform);
            bullet.transform.localPosition = localPos;

            //  총알 자체는 이동 안 하게 해야 함
            EnemyBullet b = bullet.GetComponent<EnemyBullet>();
            b.enabled = false;
        }
    }
}