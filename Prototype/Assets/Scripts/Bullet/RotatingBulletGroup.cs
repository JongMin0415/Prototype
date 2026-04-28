using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBulletGroup : MonoBehaviour
{
    public float rotateSpeed = 100f;
    private Vector2 fireDir;

    public void Init(Vector2 dir)
    {
        fireDir = dir;
        StartCoroutine(FireAfterDelay());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    IEnumerator FireAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        foreach (Transform child in transform)
        {
            EnemyBullet bullet = child.GetComponent<EnemyBullet>();

            if (bullet != null)
            {
                Vector2 dir = child.localPosition.normalized;

                child.SetParent(null);
                bullet.enabled = true;
                bullet.SetDirection(dir);
            }
        }

        Destroy(gameObject);
    }
}