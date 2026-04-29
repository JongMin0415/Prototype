using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public Room startRoom;

    [Header("Room Pools")]
    public GameObject[] easyRooms;
    public GameObject[] mediumRooms;
    public GameObject[] hardRooms;

    public GameObject bossRoom;

    public int roomCount = 6;
    public float roomWidth = 20f;

    private List<GameObject> spawnedRooms = new List<GameObject>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        Vector2 position = startRoom.transform.position;
        Room prevRoom = startRoom;

        List<GameObject> easyPool = new List<GameObject>(easyRooms);
        List<GameObject> mediumPool = new List<GameObject>(mediumRooms);
        List<GameObject> hardPool = new List<GameObject>(hardRooms);

        for (int i = 0; i < roomCount - 2; i++)
        {
            position += new Vector2(roomWidth, 0);

            GameObject roomPrefab = null;

            if (i == 0)
            {
                roomPrefab = GetRandomRoom(easyPool, easyRooms);
            }
            else if (i <= 2)
            {
                roomPrefab = GetRandomRoom(mediumPool, mediumRooms);
            }
            else
            {
                roomPrefab = GetRandomRoom(hardPool, hardRooms);
            }

            GameObject newRoomObj = Instantiate(roomPrefab, position, Quaternion.identity);
            spawnedRooms.Add(newRoomObj);

            Room newRoom = newRoomObj.GetComponent<Room>();
            prevRoom.nextRoomPoint = newRoom.entryPoint;

            prevRoom = newRoom;
        }

        position += new Vector2(roomWidth, 0);

        GameObject bossObj = Instantiate(bossRoom, position, Quaternion.identity);
        spawnedRooms.Add(bossObj);

        Room bossRoomScript = bossObj.GetComponent<Room>();
        prevRoom.nextRoomPoint = bossRoomScript.entryPoint;
    }

    GameObject GetRandomRoom(List<GameObject> pool, GameObject[] originalArray)
    {
        if (pool.Count == 0)
        {
            pool.AddRange(originalArray);
        }

        int index = Random.Range(0, pool.Count);
        GameObject selected = pool[index];
        pool.RemoveAt(index);

        return selected;
    }
}