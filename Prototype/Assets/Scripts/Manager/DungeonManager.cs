using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public Room startRoom; // 씬에 있는 StartRoom 연결

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

        // Normal 방 생성
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

            // 다음 방 연결
            prevRoom.nextRoomPoint = newRoom.transform;

            prevRoom = newRoom;
        }

        // Boss 방 생성
        position += new Vector2(roomWidth, 0);

        GameObject bossObj = Instantiate(bossRoom, position, Quaternion.identity);
        spawnedRooms.Add(bossObj);

        Room bossRoomScript = bossObj.GetComponent<Room>();

        // 마지막 연결
        prevRoom.nextRoomPoint = bossRoomScript.transform;
    }
}