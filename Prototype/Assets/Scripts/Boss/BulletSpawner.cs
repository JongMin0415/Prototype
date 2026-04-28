using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Transform firePoint;
    private bool canMove = false;
    public void Stop()
    {
        canMove = false;
    }
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