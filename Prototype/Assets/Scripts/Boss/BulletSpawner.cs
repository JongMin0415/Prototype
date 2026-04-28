using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Transform firePoint;


    public void SpawnBullet(Vector2 dir)
    {
        GameObject bullet = ObjectPool.Instance.GetEnemyBullet();

        if (bullet == null)
        {
            Debug.LogWarning("EnemyBullet «Æ ∫Œ¡∑!");
            return;
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;

        EnemyBullet b = bullet.GetComponent<EnemyBullet>();

        b.Stop();
        b.SetDirection(dir); 
    }
}