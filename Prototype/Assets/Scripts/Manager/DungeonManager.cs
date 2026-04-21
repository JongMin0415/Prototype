using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject startRoom;
    public GameObject[] normalRooms;
    public GameObject bossRoom;

    public int roomCount = 5;

    public float roomWidth = 20f;

    private List<GameObject> spawnedRooms = new List<GameObject>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        Vector2 position = Vector2.zero;

        // Start 号 持失
        GameObject start = Instantiate(startRoom, position, Quaternion.identity);
        spawnedRooms.Add(start);

        // Normal 号級 持失
        for (int i = 1; i < roomCount - 1; i++)
        {
            position += new Vector2(roomWidth, 0);

            GameObject randomRoom = normalRooms[Random.Range(0, normalRooms.Length)];
            GameObject room = Instantiate(randomRoom, position, Quaternion.identity);

            spawnedRooms.Add(room);
        }

        // Boss 号 持失
        position += new Vector2(roomWidth, 0);

        GameObject boss = Instantiate(bossRoom, position, Quaternion.identity);
        spawnedRooms.Add(boss);
    }
}