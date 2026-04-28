using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Type")]
    public bool isStartRoom = false;

    [Header("Enemy")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [Header("Portal")]
    public GameObject portalPrefab;
    public Transform portalSpawnPoint;

    [Header("Room Connection")]
    public Transform nextRoomPoint; 
    public Transform entryPoint; 

    private bool hasEntered = false;
    private bool cleared = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasEntered) return;

        if (collision.CompareTag("Player"))
        {
            hasEntered = true;

            if (isStartRoom)
            {
                SpawnPortal();
                return;
            }

            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject enemy = Instantiate(
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                point.position,
                Quaternion.identity
            );

            spawnedEnemies.Add(enemy);
        }

        StartCoroutine(CheckClear());
    }

    IEnumerator CheckClear()
    {
        while (!cleared)
        {
            spawnedEnemies.RemoveAll(e => e == null);

            if (spawnedEnemies.Count == 0)
            {
                cleared = true;
                SpawnPortal();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnPortal()
    {
        GameObject portal = Instantiate(
            portalPrefab,
            portalSpawnPoint.position,
            Quaternion.identity
        );

        Portal portalScript = portal.GetComponent<Portal>();
        portalScript.SetTarget(nextRoomPoint);
    }
}