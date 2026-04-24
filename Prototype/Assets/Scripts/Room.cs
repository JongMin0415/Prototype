using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Type")]
    public bool isStartRoom = false; //  시작 방 여부

    [Header("Enemy")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [Header("Portal")]
    public GameObject portalPrefab;
    public Transform portalSpawnPoint;
    public Transform nextRoomPoint;

    private bool hasEntered = false;
    private bool cleared = false;

    //  플레이어 입장 감지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasEntered) return;

        if (collision.CompareTag("Player"))
        {
            hasEntered = true;

            //  StartRoom이면 적 스폰 안 함
            if (isStartRoom)
            {
                SpawnPortal();
                return;
            }

            SpawnEnemies();
        }
    }

    //  적 스폰
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

    //  클리어 체크
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

    //  포탈 생성
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