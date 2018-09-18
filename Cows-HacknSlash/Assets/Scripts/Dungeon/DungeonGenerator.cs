using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour {

    [Header("Prefabs, Room lists are set up to work with more than 1 room per orientation")]
    public List<Room> Rooms;
    public GameObject Ground;
    [Header("Dimensions of the dungeon")]
    public Vector2 DungeonSize;
    public Vector2 RoomSize;
    public int RoomCount;
    [Header("Other generation factors")]
    public int ChanceToGoBack;

    public GameObject Player;

    private NavMeshSurface surface;

    void Start()
    {
        StartCoroutine("GenerateDungeon");
    }

    IEnumerator GenerateDungeon ()
    {
        // Loads the dungeon.
        bool[,] roomLayout = new bool[(int)DungeonSize.x, (int)DungeonSize.y];
        Vector2 currentPos = new Vector2(DungeonSize.x / 2, DungeonSize.y / 2);
        //For Navmesh Later
        Vector2 startPos = currentPos;

        // Spawn the ground.
        GameObject ground = Instantiate(Ground, new Vector3(currentPos.x * RoomSize.x, 0, currentPos.y * RoomSize.x), Quaternion.identity, this.transform);
        ground.transform.localScale = new Vector3(DungeonSize.x / 10, 1, DungeonSize.x / 10);
        surface = ground.gameObject.GetComponent<NavMeshSurface>();

        // Start spawning the dungeon.
        for (int i = 0; i < RoomCount; i++)
        {
            int rn = Random.Range(0, 100);

            // Choose the new direction.
            if (rn < 26) {
                currentPos.y -= 1;
            } else if (rn < 51) {
                currentPos.x -= 1;
            } else if (rn < 76) {
                currentPos.x += 1;
            } else if (rn < 101) {
                currentPos.y += 1;
            }

            // If the room overlaps, large chance to go back.
            if (roomLayout[(int)currentPos.x, (int)currentPos.y] == true && Random.Range(1, 100) < ChanceToGoBack) {
                if (rn < 26) {
                    currentPos.y += 1;
                }
                if (rn < 51) {
                    currentPos.x += 1;
                }
                if (rn < 76) {
                    currentPos.x -= 1;
                }
                if (rn < 101) {
                    currentPos.y -= 1;
                }

                i ++;
            }
            // Add the room to the list of true rooms.
            roomLayout[(int)currentPos.x, (int)currentPos.y] = true;
        }

        // Loop through each room pos and spawn one in.
        for (int x = 0; x < DungeonSize.x; x++)
        {
            for (int y = 0; y < DungeonSize.y; y++)
            {
                if (roomLayout[x, y] == true)
                {
                    SpawnRoom(x, y, roomLayout);

                    // So unity doesn't crash because spawning 1k + prefabs with lots of calculations in a single frame isn't good for unity :(
                    yield return new WaitForSeconds(0.001f);
                }
            }
        }
        //Navmesh Code
        Vector2 groundPos;

        groundPos.x = (startPos.x + currentPos.x) / 2;
        groundPos.y = (startPos.y + currentPos.y) / 2;

        ground.transform.position = new Vector3(groundPos.x * RoomSize.x, 0, groundPos.y * RoomSize.y);
        surface.BuildNavMesh();

        //Spawn The Player
        Instantiate(Player, new Vector3(startPos.x * RoomSize.x, 1, startPos.y * RoomSize.y), Quaternion.identity);
    }

    void SpawnRoom(int x, int y, bool[,] roomLayout)
    {
        // Just a little somthing I made to make the room that spawn the correct room.
        foreach (Room room in Rooms)
        {
            if (room.OpenBottom == roomLayout[x, y - 1] &&
                room.OpenLeft == roomLayout[x - 1, y] &&
                room.OpenRight == roomLayout[x + 1, y] &&
                room.OpenTop == roomLayout[x, y + 1])
            {
                if (room.Rooms[0] == null) {
                    return;
                }
                Instantiate(room.Rooms[(int)Random.Range(0, room.Rooms.Count)], new Vector3(x * RoomSize.x, 0, y * RoomSize.y), Quaternion.identity, this.transform);
            }
        }
    }
}
