using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Basement";
    RoomInfo currentLoadRoomData;
    Queue<RoomInfo> loadRoomQueue = new();
    public List<Room> loadedRooms = new();
    bool isLoadingRoom = false;

    void Awake()
    {
        instance = this;
    }

    public bool DoesRoomExists(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }

}
