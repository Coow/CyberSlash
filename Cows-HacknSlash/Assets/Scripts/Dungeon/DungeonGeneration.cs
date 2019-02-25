using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGeneration : MonoBehaviour {

    [Header("Prefabs of rooms, can place multiple rooms in 1 list")]
    public List<Room> EasyRooms;
    public List<Room> NormalRooms;
    public List<Room> HardRooms;
    [Tooltip("1 = easy, 2 = med, 3 = high")]
    public int Difficulty;
    [Header("Amount of rooms, will affect the size of the dungeon")]
    public int RoomCount;
    public int RoomSize;
    [Header("Will also affect dungeon size, more explination in code")]
    //Affects the chance of a room trying to spawn on another room (on the list), will affect the spread of the dungeon
    public int ChanceToGoBack;
    [Header("Prefab of the player")]
    public GameObject Player;

    public GameObject testBlock;

    private bool[,] roomArray;

    private int codeToWorld = 1000;
    private Vector2 currentRoomPos;

    void Awake()
    {
        //Init the array + the starting position
        roomArray = new bool[codeToWorld * 2, codeToWorld * 2];
        currentRoomPos = new Vector2(codeToWorld, codeToWorld);
        roomArray[codeToWorld, codeToWorld] = true;

        populateArray();
        StartCoroutine("spawnRooms");
    }

    private void populateArray()
    {
        for (int i = 0; i < RoomCount; i++)
        {
            //For each room needed to spawn, move the 'player' in a random direction
            int rand = Random.Range(0, 100);
            //So if the new room is on another room we can go back
            Vector2 oldPos = currentRoomPos;
            //Chose a random direction for the player to go in and go there
            if (rand <= 25)
            { currentRoomPos.x++; }
            else if (rand <= 50)
            { currentRoomPos.x--; }
            else if (rand <= 75)
            { currentRoomPos.y++; }
            else { currentRoomPos.y--; }

            //Check to see if there is allready a room there
            if (roomArray[(int)currentRoomPos.x, (int)currentRoomPos.y] == true && Random.Range(0, 100) < ChanceToGoBack)
            {
                //Have a chance to disregaard the movment and try again
                i--;
                currentRoomPos = oldPos;
            }
            else
            {
                //Tell the computer to spawn in a room at the new position and then start again
                roomArray[(int)currentRoomPos.x, (int)currentRoomPos.y] = true;
            }
        }
    }

    private IEnumerator spawnRooms()
    {
        //Loop through each position, and if there is a room there, spawn it in
        for (int x = 0; x < codeToWorld * 2; x++)
        {
            for (int y = 0; y < codeToWorld * 2; y++)
            {
                if (roomArray[x, y] == true)
                {
                    GameObject newRoom = Instantiate(chooseRoomToSpawn(x, y), new Vector3((x - codeToWorld) * RoomSize, 0, (y - codeToWorld) * RoomSize), 
                        Quaternion.identity, this.transform);
                    if (x - codeToWorld == 0 & y - codeToWorld == 0) { newRoom.name = "0, 0 Room!!!"; }
                    newRoom.transform.Rotate(0, 180, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        //Add The NavMesh
        StartCoroutine("addNavmesh");
    }
    
    private GameObject chooseRoomToSpawn (int x, int y)
    {
        //Check to see what sides there are rooms on
        bool b =  roomArray[x, y - 1];
        bool l =  roomArray[x - 1, y];
        bool r = roomArray[x + 1, y];
        bool t = roomArray[x, y + 1];
        //Select the room list based on difficulty
        List<Room> roomSet = EasyRooms;
        if (Difficulty == 2) { roomSet = NormalRooms; }
        if (Difficulty == 3) { roomSet = HardRooms; }
        //Select only a room that has openings to all rooms beside this one(open to all adjacent rooms)
        foreach (Room room in roomSet)
        {
            if(b == room.OpenBottom && l == room.OpenLeft && r == room.OpenRight && t == room.OpenTop)
            {
                return (room.Rooms[Random.Range(0, room.Rooms.Count)]);
            }
        }
        return (testBlock);
    }

    private IEnumerator addNavmesh ()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //Add The NavMesh implementation Here(In The If Statement, Before The yeild return waitForSeconds)
            //Just use 'this.transform.getchild(i)' to get the rooms
            yield return new WaitForSeconds(0.01f);
        }
    }
}
