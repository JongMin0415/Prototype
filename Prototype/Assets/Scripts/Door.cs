using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform targetPoint; // 다음 방 위치
    public bool isOpen = false;

    public void SetTarget(Transform target)
    {
        targetPoint = target;
    }

    public void OpenDoor()
    {
        isOpen = true;

        // ?? 필요하면 문 비활성화
        // gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen) return;

        if (collision.CompareTag("Player"))
        {
            collision.transform.position = targetPoint.position;
        }
    }
}