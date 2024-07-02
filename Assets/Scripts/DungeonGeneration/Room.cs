using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int x;
    public int y;
    private bool updatedDoors = false;
    public Room(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    public List<Door> doors = new();
    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            d.gameObject.SetActive(false);
            switch (d.doorType)
            {
                case Door.DoorType.left:
                    leftDoor = d;
                    doors.Add(leftDoor);
                    break;
                case Door.DoorType.right:
                    rightDoor = d;
                    doors.Add(rightDoor);
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    doors.Add(topDoor);
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    doors.Add(bottomDoor);
                    break;
            }
        }
        RoomController.instance.RegisterRoom(this);
        UpdateDoors();
        InimigosExistem();
    }

    bool InimigosExistem()
    {
        if (GetComponentInChildren<EnemyController>())
        {
            return true;
        }

        return false;
    }

    void Update()
    {
        if (InimigosExistem() && RoomController.instance.currRoom == this)
        {
            DesativarPortas();
        }
        else if (!InimigosExistem() && RoomController.instance.currRoom == this)
        {
            ReativarPortas();
        }
        else if (RoomController.instance.currRoom != this)
        {
            DesativarPortas();
        }
        //     if (GetComponent<ObjectRoomSpawner>().spawnerData.Length != 0)
        //     {
        //         foreach (Door door in doors)
        //         { door.gameObject.SetActive(false); };
        //     }
        //     else
        //     {
        //         ReativarPortas();
        //     }
        //     //     if (name.Contains("End") && !updatedDoors)
        //     //     {
        //     //         UpdateDoors();
        //     //         updatedDoors = true;
        //     //     }
    }

    public void DesativarPortas()
    {
        foreach (Door door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }

    public void UpdateDoors()
    {
        List<RoomInfo> salasExistentes = RoomController.instance.loadRoomQueue;
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    if (!RoomController.instance.DoesRoomExistsInQueue(x - 1, y))
                    {
                        door.gameObject.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(true);
                    }
                    break;
                case Door.DoorType.right:
                    if (!RoomController.instance.DoesRoomExistsInQueue(x + 1, y))
                    {
                        door.gameObject.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(true);
                    }
                    break;
                case Door.DoorType.top:
                    if (!RoomController.instance.DoesRoomExistsInQueue(x, y + 1))
                    {
                        door.gameObject.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(true);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (!RoomController.instance.DoesRoomExistsInQueue(x, y - 1))
                    {
                        door.gameObject.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }

    // public Room GetRight()
    // {
    //     if (RoomController.instance.DoesRoomExistsInList(x + 1, y))
    //     {
    //         return RoomController.instance.FindRoom(x + 1, y);
    //     }
    //     return null;

    // }
    // public Room GetLeft()
    // {
    //     if (RoomController.instance.DoesRoomExistsInList(x - 1, y))
    //     {
    //         return RoomController.instance.FindRoom(x - 1, y);
    //     }
    //     return null;
    // }
    // public Room GetTop()
    // {
    //     if (RoomController.instance.DoesRoomExistsInList(x, y + 1))
    //     {
    //         return RoomController.instance.FindRoom(x, y + 1);
    //     }
    //     return null;
    // }
    // public Room GetDown()
    // {
    //     if (RoomController.instance.DoesRoomExistsInList(x, y - 1))
    //     {
    //         return RoomController.instance.FindRoom(x, y - 1);
    //     }
    //     return null;
    // }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(x * width, y * height);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            RoomController.instance.PlayerVaiSairSala(this);
        }
    }

    public void ReativarPortas()
    {
        if (RoomController.instance.currRoom == this)
        {
            foreach (Door door in doors)
            {
                switch (door.doorType)
                {
                    case Door.DoorType.left:
                        if (RoomController.instance.DoesRoomExistsInList(x - 1, y) || RoomController.instance.DoesRoomExistsInQueue(x - 1, y))
                        {
                            door.gameObject.SetActive(true);
                        }
                        break;
                    case Door.DoorType.right:
                        if (RoomController.instance.DoesRoomExistsInList(x + 1, y) || RoomController.instance.DoesRoomExistsInQueue(x + 1, y))
                        {
                            door.gameObject.SetActive(true);
                        }
                        break;
                    case Door.DoorType.top:
                        if (RoomController.instance.DoesRoomExistsInList(x, y + 1) || RoomController.instance.DoesRoomExistsInQueue(x, y + 1))
                        {
                            door.gameObject.SetActive(true);
                        }
                        break;
                    case Door.DoorType.bottom:
                        if (RoomController.instance.DoesRoomExistsInList(x, y - 1) || RoomController.instance.DoesRoomExistsInQueue(x, y - 1))
                        {
                            door.gameObject.SetActive(true);
                        }
                        break;
                }
            }
        }
    }
}
