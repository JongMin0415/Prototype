using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;

    public int poolSize = 50;

    private Queue<GameObject> playerBullets = new Queue<GameObject>();
    private Queue<GameObject> enemyBullets = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        // 플레이어 총알 풀
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(playerBulletPrefab);
            obj.SetActive(false);
            playerBullets.Enqueue(obj);
        }

        // 적 총알 풀
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(enemyBulletPrefab);
            obj.SetActive(false);
            enemyBullets.Enqueue(obj);
        }
    }

    public GameObject GetPlayerBullet()
    {
        if (playerBullets.Count > 0)
        {
            GameObject obj = playerBullets.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public GameObject GetEnemyBullet()
    {
        if (enemyBullets.Count > 0)
        {
            GameObject obj = enemyBullets.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public void ReturnPlayerBullet(GameObject obj)
    {
        obj.SetActive(false);
        playerBullets.Enqueue(obj);
    }

    public void ReturnEnemyBullet(GameObject obj)
    {
        obj.SetActive(false);
        enemyBullets.Enqueue(obj);
    }
}