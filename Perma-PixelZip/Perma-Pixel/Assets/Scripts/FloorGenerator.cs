using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public int[,] map;
    public int[,] croppedMap;
    public int numberOfRooms = 100;
    public float roomWidth = 3f;
    public float roomHeight = 3f;
    public GameObject Room;
    public GameObject StartRoom;
    public GameObject EndRoom;
    public GameObject Walls;
    public GameObject EnemyRoom;
    public GameObject Player;
    public GameObject Cam;
    public Vector2 spawnCoords;

    public void Start()
    {
        Generate();
        PlayerToSpawn();
    }

    public void Generate()
    {
        //Creates An Extra Large 2D Array To Prevent Reaching Out Of Bounds
        map = new int[numberOfRooms * 2 + 1, numberOfRooms * 2 + 1];

        //Creates The Boss And Starting Rooms
        map[numberOfRooms, numberOfRooms] = 1;

        //Creates A List Of Known Rooms And Adds The Starting Room To It
        List<RoomLocation> roomLocs = new List<RoomLocation>();
        List<RoomLocation> surRoomLocs = new List<RoomLocation>();
        roomLocs.Add(new RoomLocation(numberOfRooms, numberOfRooms, false));

        int roomsCompleted = 1;
        int ups = 0;
        int downs = 0;
        int lefts = 0;
        int rights = 0;
        float thres = 2;
        float numOfRuns = 0;
        while (roomsCompleted < numberOfRooms)
        {
            //Chooses A Random Room From The List Of Known Rooms To Build Off Of
            int roomAddingTo = Random.Range(0, roomLocs.Count);
            //Randomly Chooses To Build To The Left, Right, Or Bottom
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                //If Building Left
                case 0:
                    if (map[roomLocs[roomAddingTo].row - 1, roomLocs[roomAddingTo].col] == 0)
                    {
                        map[roomLocs[roomAddingTo].row - 1, roomLocs[roomAddingTo].col] = 1;
                        roomLocs.Add(new RoomLocation(roomLocs[roomAddingTo].row - 1, roomLocs[roomAddingTo].col, false));
                        roomsCompleted += 1;
                        lefts++;
                    }
                    break;

                //If Building Right
                case 1:
                    if (map[roomLocs[roomAddingTo].row + 1, roomLocs[roomAddingTo].col] == 0)
                    {
                        map[roomLocs[roomAddingTo].row + 1, roomLocs[roomAddingTo].col] = 1;
                        roomLocs.Add(new RoomLocation(roomLocs[roomAddingTo].row + 1, roomLocs[roomAddingTo].col, false));
                        roomsCompleted += 1;
                        rights++;
                    }
                    break;

                //If Building Below
                case 2:
                    if (map[roomLocs[roomAddingTo].row, roomLocs[roomAddingTo].col + 1] == 0)
                    {
                        map[roomLocs[roomAddingTo].row, roomLocs[roomAddingTo].col + 1] = 1;
                        roomLocs.Add(new RoomLocation(roomLocs[roomAddingTo].row, roomLocs[roomAddingTo].col + 1, false));
                        roomsCompleted += 1;
                        downs++;
                    }
                    break;
                //If Building Above
                case 3:
                    if (map[roomLocs[roomAddingTo].row, roomLocs[roomAddingTo].col - 1] == 0)
                    {
                        map[roomLocs[roomAddingTo].row, roomLocs[roomAddingTo].col - 1] = 1;
                        roomLocs.Add(new RoomLocation(roomLocs[roomAddingTo].row, roomLocs[roomAddingTo].col - 1, false));
                        roomsCompleted += 1;
                        ups++;
                    }
                    break;
            }
            numOfRuns++;
            //Removes Completely Surrounded Rooms From The List Of Rooms To Decrease Randomness
            for (int i = 0; i < roomLocs.Count; i++)
            {
                if (!roomLocs[i].surrounded)
                {
                    int count = 0;
                    count += map[roomLocs[i].row, roomLocs[i].col + 1];
                    count += map[roomLocs[i].row, roomLocs[i].col - 1];
                    count += map[roomLocs[i].row + 1, roomLocs[i].col];
                    count += map[roomLocs[i].row - 1, roomLocs[i].col];
                    if (count > thres && roomLocs.Count > 2)
                    {
                        surRoomLocs.Add(roomLocs[i]);
                        roomLocs.Remove(roomLocs[i]);
                    }
                }
            }

            if (roomLocs.Count < 2 && thres != 3)
            {
                thres = 3;
                roomLocs.AddRange(surRoomLocs);
                surRoomLocs.Clear();
            }
            else if (numOfRuns > 12000)
            {
                break;
            }
        }
        //Crops Map
        int minRow = 0;
        int maxRow = numberOfRooms * 2;
        int minCol = 0;
        int maxCol = numberOfRooms * 2;

        //Find Min Row
        for (int i = numberOfRooms * 2; i > 0; i--)
        {
            for (int k = numberOfRooms * 2; k > 0; k--)
            {
                if (map[i, k] != 0)
                {
                    minRow = i;
                }
            }
        }

        //Find Max Row
        for (int i = 0; i < numberOfRooms * 2; i++)
        {
            for (int k = 0; k < numberOfRooms * 2; k++)
            {
                if (map[i, k] != 0)
                {
                    maxRow = i;
                }
            }
        }

        //Find Min Column
        for (int i = numberOfRooms * 2; i > 0; i--)
        {
            for (int k = numberOfRooms * 2; k > 0; k--)
            {
                if (map[k, i] != 0)
                {
                    minCol = i;
                }
            }
        }
        //Find Max Column
        for (int i = 0; i < numberOfRooms * 2; i++)
        {
            for (int k = 0; k < numberOfRooms * 2; k++)
            {
                if (map[k, i] != 0)
                {
                    maxCol = i;
                }
            }
        }

        //Creates A Cropped Map And Fills It In
        croppedMap = new int[maxRow - minRow + 3, maxCol - minCol + 3];
        for (int i = 0; i < croppedMap.GetLength(0) - 2; i++)
        {
            for (int k = 0; k < croppedMap.GetLength(1) - 2; k++)
            {
                croppedMap[i + 1, k + 1] = map[i + minRow, k + minCol];
            }
        }
        map = croppedMap;

        //Recreates A List Of Rooms
        List<Room> rooms = new List<Room>();

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int k = 0; k < map.GetLength(1); k++)
            {
                if (map[i, k] == 1)
                {
                    rooms.Add(new Room(i, k));
                }
            }
        }
        List<Room> walls = new List<Room>();
        //Makes Outline Of Walls
        foreach (Room r in rooms)
        {
            if(map[r.row - 1, r.col] == 0)
            {
                map[r.row - 1, r.col] = 4;
                walls.Add(new Room(r.row - 1, r.col));
            }
            if (map[r.row + 1, r.col] == 0)
            {
                map[r.row + 1, r.col] = 4;
                walls.Add(new Room(r.row + 1, r.col));
            }
            if (map[r.row, r.col - 1] == 0)
            {
                map[r.row, r.col - 1] = 4;
                walls.Add(new Room(r.row, r.col - 1));
            }
            if (map[r.row, r.col + 1] == 0)
            {
                map[r.row, r.col + 1] = 4;
                walls.Add(new Room(r.row, r.col + 1));
            }

            if(map[r.row - 1, r.col - 1] == 0)
            {
                map[r.row - 1, r.col - 1] = 4;
                walls.Add(new Room(r.row - 1, r.col - 1));
            }
            if (map[r.row + 1, r.col - 1] == 0)
            {
                map[r.row + 1, r.col - 1] = 4;
                walls.Add(new Room(r.row + 1, r.col - 1));
            }
            if (map[r.row - 1, r.col + 1] == 0)
            {
                map[r.row - 1, r.col + 1] = 4;
                walls.Add(new Room(r.row - 1, r.col + 1));
            }
            if (map[r.row + 1, r.col + 1] == 0)
            {
                map[r.row + 1, r.col + 1] = 4;
                walls.Add(new Room(r.row + 1, r.col + 1));
            }
        }

        //Sets Starting Room
        int randRoom = Random.Range(0, rooms.Count);
        map[rooms[randRoom].row, rooms[randRoom].col] = 2;
        spawnCoords = new Vector2(rooms[randRoom].col * (roomWidth), -rooms[randRoom].row * (roomHeight));

        //Sets End Room
        bool doneExit = false;
        float loweringThres = 1;

        int randExit = 0;
        while (!doneExit)
        {
            randExit = Random.Range(0, rooms.Count);
            int randExitR = rooms[randExit].row;
            int randExitC = rooms[randExit].col;
            int randStartR = rooms[randRoom].row;
            int randStartC = rooms[randRoom].col;
            float distance = Vector2.Distance(new Vector2(randExitC, randExitR), new Vector2(randStartC, randStartR));
            float board = Vector2.Distance(new Vector2(0, 0), new Vector2(map.GetLength(0), map.GetLength(1)));
            if (distance > board - loweringThres)
            {
                Debug.Log(randExit);
                map[rooms[randExit].row, rooms[randExit].col] = 3;
                doneExit = true;
            }
            loweringThres += .1f;
            if (Mathf.Abs(board - loweringThres) < 1)
            {
                map[rooms[randExit].row, rooms[randExit].col] = 3;
                break;
            }
        }

        //Generates Monster Rooms
        int enemyRooms = 0;
        int loops = 0;
        while(enemyRooms < numberOfRooms/8)
        {
            foreach (Room r in rooms)
            {
                if(map[r.row, r.col] != 5 && map[r.row, r.col] != 3)
                {
                    float randomChance = Random.Range(0, 6.5f);
                    if (randomChance <= 1 && Mathf.Abs(rooms[randRoom].row - r.row) + Mathf.Abs(rooms[randRoom].col - r.col) > 2 && Mathf.Abs(rooms[randExit].row - r.row) + Mathf.Abs(rooms[randExit].col - r.col) > 2)
                    {
                        if (randomChance < .2f || (map[r.row - 1, r.col] != 5 && map[r.row + 1, r.col] != 5 && map[r.row, r.col - 1] != 5 && map[r.row - 1, r.col + 1] != 5))
                        {
                            map[r.row, r.col] = 5;
                            enemyRooms++;
                        }
                    }
                }
            }
            loops++;
            if(loops > 100)
            {
                break;
            }
        }


        foreach (Room r in rooms)
        {
            if (map[r.row, r.col] == 1)
            {
                Instantiate(Room, new Vector3(r.col * (roomWidth), -r.row * (roomHeight), 0), Quaternion.identity);
            }
            else if (map[r.row, r.col] == 2)
            {
                Instantiate(StartRoom, new Vector3(r.col * (roomWidth), -r.row * (roomHeight), 0), Quaternion.identity);
            }
            else if (map[r.row, r.col] == 3)
            {
                Instantiate(EndRoom, new Vector3(r.col * (roomWidth), -r.row * (roomHeight), 0), Quaternion.identity);
            }
            else if (map[r.row, r.col] == 5)
            {
                Instantiate(EnemyRoom, new Vector3(r.col * (roomWidth), -r.row * (roomHeight), 0), Quaternion.identity);
            }
        }
        foreach (Room r in walls)
        {
            Instantiate(Walls, new Vector3(r.col * (roomWidth), -r.row * (roomHeight), 0), Quaternion.identity);
        }
    }

    public void PlayerToSpawn()
    {
        Player.transform.position = spawnCoords;
    }
}

public class RoomLocation
{
    public int row;
    public int col;
    public bool surrounded;

    public RoomLocation(int nRow, int nCol, bool nSurrounded)
    {
        row = nRow;
        col = nCol;
        surrounded = nSurrounded;
    }
}

public class Room
{
    public int row;
    public int col;

    public Room(int nRow, int nCol)
    {
        row = nRow;
        col = nCol;
    }
}
