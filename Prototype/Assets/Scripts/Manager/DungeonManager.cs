using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public Room startRoom;

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
        Vector2 position = startRoom.transform.position;

        Room prevRoom = startRoom;

        for (int i = 0; i < roomCount - 2; i++)
        {
            position += new Vector2(roomWidth, 0);

            GameObject newRoomObj = Instantiate(
                normalRooms[Random.Range(0, normalRooms.Length)],
                position,
                Quaternion.identity
            );

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
}