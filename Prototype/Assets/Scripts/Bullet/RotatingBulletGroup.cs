using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBulletGroup : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    private Vector2 moveDir;

    public void Init(Vector2 dir)
    {
        moveDir = dir.normalized;
    }

    void Update()
    {
        //  전체 이동 (플레이어 방향)
        transform.position += (Vector3)(moveDir * moveSpeed * Time.deltaTime);

        //  전체 회전
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}