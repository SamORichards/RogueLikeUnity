using UnityEngine;
using System.Collections.Generic;

public class TMData {
    int maxTries = 0;
    protected class DRoom{
        public int left;
        public int top;
        public int width;
        public int height;

        public int right
        {
            get { return left + width - 1; }
        }
        public int bottom
        {
            get { return top + height - 1; }
        }
        public int centre_y {

            get { return top + height / 2; }
        }
        public int centre_x
        {

            get { return left + width / 2; }
        }

        internal bool CollidesWith(DRoom r2)
        {
            if (left > r2.right)
            { 
                return false;
            }

            if (top > r2.bottom)
            {
                return false;
            }

            if (right < r2.left - 1)
            {
                return false;
            }

            if (bottom < r2.top - 1)
            {
                return false;
            }

            return true;
        }
        public bool isConnected = false;
    }



    List<DRoom> Room_List;

    int[,] Map_data;
    int size_y, size_x;


    public enum TileType {
        BLANK,
        STONE_TILE,
        FLOOR_TILE,
        WALL_TILE
    }

    public TMData(int size_y, int size_x) {
        this.size_y = size_y;
        this.size_x = size_x;
        int numRooms = size_x / 5;
        Map_data = new int[size_x, size_y];
        for (int x = 0; x < size_x; x++) {
            for (int y = 0; y < size_y; y++)
            {
                Map_data[x, y] = (int)TileType.STONE_TILE;
            }
        }
        Room_List = new List<DRoom>();
        while(numRooms > 0)
        {
            int RX = Random.Range(Random.Range(25, 40), 7);
            int RY = Random.Range(Random.Range(15, Random.Range(18,35)), 6);
            DRoom r = new DRoom();
            r.left = Random.Range(1, size_x - RX -1);
            r.top = Random.Range(1, size_y - RY -1);
            r.width = RX;
            r.height = RY;

            if (!RoomCollides(r))
            {
                Room_List.Add(r);
                numRooms--;
            }
            else {
                maxTries++;
                if (maxTries > 1000) {
                    Debug.Log("Failed to Gen all rooms");
                    break;
                }
            }
        }
        foreach (DRoom r2 in Room_List) {
            MakeRoom(r2);
        }
        for (int count =0; count < Room_List.Count; count++)
        {
            int room2 =0;
            bool isntSameRoom = false;
            while (!isntSameRoom) {
                room2 = Random.Range(0, Room_List.Count);
                if (room2 != count)
                {
                    isntSameRoom = true;
                }
            }
            if (Room_List[count].isConnected == false)
            {
                    makeCorridor(Room_List[count], Room_List[room2]);
            }
        }
        for (int x = 0; x < size_x -1; x++)
        {
            for (int y = 0; y < size_y -1; y++)
            {
                if (Map_data[x, y] == (int)TileType.STONE_TILE)
                {
                    int x1 = x+1;
                    int x2 = x-1;
                    int y1 = y+1;
                    int y2 = y-1;
                    if (x != size_x)
                    {
                        if (Map_data[x1, y] == (int)TileType.FLOOR_TILE)
                        {
                            Map_data[x, y] = (int)TileType.WALL_TILE;
                        }
                    }
                    if (y != size_y)
                    {
                        if (Map_data[x, y1] == (int)TileType.FLOOR_TILE)
                        {
                            Map_data[x, y] = (int)TileType.WALL_TILE;
                        }
                    }
                    if (y != 0)
                    {
                        if (Map_data[x, y2] == (int)TileType.FLOOR_TILE)
                        {
                            Map_data[x, y] = (int)TileType.WALL_TILE;
                        }
                    }
                    if(x != 0) { 
                        if (Map_data[x2, y] == (int)TileType.FLOOR_TILE)
                        {
                            Map_data[x, y] = (int)TileType.WALL_TILE;
                        }
                    }
                }
            }
        }
    }

    private void makeCorridor(DRoom rm1, DRoom rm2)
    {
        int x, y;
        x = rm1.centre_x;
        y = rm1.centre_y;
            while (x != rm2.centre_x) {
                if (x > rm2.centre_x)
                {
                    x--;
                }
                else {
                    x++;
                }
                Map_data[x, y] = (int) TileType.FLOOR_TILE;
            }
            while (y != rm2.centre_y)
            {
                if (y > rm2.centre_y)
                {
                    y--;
                }
                else {
                    y++;
                }
                Map_data[x, y] = (int)TileType.FLOOR_TILE;
            }
        
        rm1.isConnected = true;
        rm2.isConnected = true;
    }

    private bool RoomCollides(DRoom r)
    {
        foreach (DRoom r2 in Room_List) {
            if (r.CollidesWith(r2)) {
                return true;
            }
        }
        return false;
    }


    public object getTileAt(int x, int y) {
        return Map_data[x, y];
    }

    void MakeRoom(DRoom r)
    {
        for (int x = 0; x < r.width; x++)
        {
            for (int y = 0; y < r.height; y++)
            {
                    Map_data[r.left + x, r.top + y] = (int)TileType.FLOOR_TILE;
            }
        }

    }
}
