using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 6f;
    private Vector2 direction;
    private bool canMove = false;

    public void Stop()
    {
        canMove = false;
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        canMove = true;

        CancelInvoke();
        Invoke("Deactivate", 3f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Deactivate()
    {
        ObjectPool.Instance.ReturnEnemyBullet(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(1);
            }

            Deactivate();
        }
    }

    void OnDisable()
    {
        CancelInvoke(); 
        transform.SetParent(null); 
        direction = Vector2.zero;  
        enabled = true;           
    }
}