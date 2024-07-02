using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public Room currRoom;
    RoomInfo roomToRemove;
    public List<RoomInfo> loadRoomQueue = new();
    public List<Room> loadedRooms = new();
    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;
    bool primeiraSalaCriada = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // LoadRoom("Start", 0, 0);
        // LoadRoom("Empty", 1, 0);
        // LoadRoom("Empty", 0, 1);
        // LoadRoom("Empty", -1, 0);
        // LoadRoom("Empty", 0, -1);
    }

    void CriarPrimeiraSala()
    {
        roomToRemove = loadRoomQueue.Single(r => r.x == 0 && r.y == 0 && r.name == "Start");
        loadRoomQueue.Remove(roomToRemove);
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(roomToRemove));
        primeiraSalaCriada = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(5);
        }
        if (!primeiraSalaCriada)
        {
            CriarPrimeiraSala();
        }
        if (!spawnedBossRoom)
        {
            CriarSalaBoss();
        }
        if (isLoadingRoom)
        {
            return;
        }

        // if (loadRoomQueue.Count == 0)
        // {
        //     if (!spawnedBossRoom)
        //     {
        //         StartCoroutine(SpawnBossRoom());
        //     }
        // }
        // else if (spawnedBossRoom && !updatedRooms)
        // {
        //     foreach (Room room in loadedRooms)
        //     {
        //         room.RemoveUnconnectedDoors();
        //     }
        UpdateRooms();
        updatedRooms = true;
        // }
        // return;
        // }

        //roomToRemove = loadRoomQueue.Dequeue();
        //isLoadingRoom = true;

        //StartCoroutine(LoadRoomRoutine(roomToRemove));
    }

    // IEnumerator SpawnBossRoom()
    // {
    //     spawnedBossRoom = true;
    //     yield return new WaitForSeconds(0.5f);
    //     if (loadRoomQueue.Count == 0)
    //     {
    //         Room bossRoom = loadedRooms[loadedRooms.Count - 1];
    //         Room tempRoom = new(bossRoom.x, bossRoom.y);
    //         Destroy(bossRoom.gameObject);
    //         var roomToRemove2 = loadedRooms.Single(r => r.x == tempRoom.x && r.y == tempRoom.y);
    //         loadedRooms.Remove(roomToRemove2);
    //         LoadRoom("End", tempRoom.x, tempRoom.y);
    //     }
    // }
    void CriarSalaBoss()
    {
        loadRoomQueue[loadRoomQueue.Count - 1].name = "End";
        spawnedBossRoom = true;

    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExistsInQueue(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new()
        {
            name = name,
            x = x,
            y = y
        };

        loadRoomQueue.Add(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExistsInList(roomToRemove.x, roomToRemove.y))
        {
            room.transform.position = new Vector3(roomToRemove.x * room.width, roomToRemove.y * room.height, 0);

            room.x = roomToRemove.x;
            room.y = roomToRemove.y;
            room.name = currentWorldName + "-" + roomToRemove.name + "-" + roomToRemove.x + roomToRemove.y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }
            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExistsInQueue(int x, int y)
    {
        return loadRoomQueue.Find(item => item.x == x && item.y == y) != null;
    }

    public bool DoesRoomExistsInList(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[] {
            "Empty",
            "Basic1",
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
        UpdateRooms();
    }

    public void PlayerVaiSairSala(Room room)
    {
        foreach (Door door in room.doors)
        {
            door.gameObject.SetActive(false);
        }
    }

    public void PassouPorta(Door door)
    {
        switch (door.doorType)
        {
            case Door.DoorType.left:
                PassouEsquerda(door.transform.position);
                break;
            case Door.DoorType.right:
                PassouDireita(door.transform.position);
                break;
            case Door.DoorType.top:
                PassouCima(door.transform.position);
                break;
            case Door.DoorType.bottom:
                PassouBaixo(door.transform.position);
                break;
        }
    }

    public void PassouEsquerda(Vector2 posicaoPorta)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(posicaoPorta.x - 2.5f, posicaoPorta.y);

        if (DoesRoomExistsInQueue(currRoom.x - 1, currRoom.y))
        {
            roomToRemove = loadRoomQueue.Single(r => r.x == currRoom.x - 1 && r.y == currRoom.y);
            loadRoomQueue.Remove(roomToRemove);
            isLoadingRoom = true;

            StartCoroutine(LoadRoomRoutine(roomToRemove));
        }
    }
    public void PassouDireita(Vector2 posicaoPorta)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(posicaoPorta.x + 2.5f, posicaoPorta.y);

        if (DoesRoomExistsInQueue(currRoom.x + 1, currRoom.y))
        {
            roomToRemove = loadRoomQueue.Single(r => r.x == currRoom.x + 1 && r.y == currRoom.y);
            loadRoomQueue.Remove(roomToRemove);
            isLoadingRoom = true;

            StartCoroutine(LoadRoomRoutine(roomToRemove));
        }
    }

    public void PassouCima(Vector2 posicaoPorta)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(posicaoPorta.x, posicaoPorta.y + 2.5f);

        if (DoesRoomExistsInQueue(currRoom.x, currRoom.y + 1))
        {
            roomToRemove = loadRoomQueue.Single(r => r.x == currRoom.x && r.y == currRoom.y + 1);

            loadRoomQueue.Remove(roomToRemove);
            isLoadingRoom = true;

            StartCoroutine(LoadRoomRoutine(roomToRemove));
        }
    }

    public void PassouBaixo(Vector2 posicaoPorta)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(posicaoPorta.x, posicaoPorta.y - 2.5f);

        if (DoesRoomExistsInQueue(currRoom.x, currRoom.y - 1))
        {
            roomToRemove = loadRoomQueue.Single(r => r.x == currRoom.x && r.y == currRoom.y - 1);
            loadRoomQueue.Remove(roomToRemove);
            isLoadingRoom = true;

            StartCoroutine(LoadRoomRoutine(roomToRemove));
        }
    }

    private void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if (currRoom != room)
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if (enemies != null)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = true;
                    }
                }
            }
            else
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if (enemies != null)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = false;
                    }
                }
            }
        }
    }

}
