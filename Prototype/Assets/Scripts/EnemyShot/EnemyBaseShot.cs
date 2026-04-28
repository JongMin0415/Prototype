using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class EnemyBaseShot : MonoBehaviour
{
    public Transform firePoint;

    protected Transform target;

    protected virtual void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    protected Vector2 GetDirection()
    {
        if (target == null) return Vector2.right;
        return (target.position - firePoint.position).normalized;
    }

    protected void SpawnBullet(Vector2 dir)
    {
        GameObject bullet = ObjectPool.Instance.GetEnemyBullet();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.identity;

            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            bulletScript.SetDirection(dir);
        }
    }

    public abstract void Fire();
}