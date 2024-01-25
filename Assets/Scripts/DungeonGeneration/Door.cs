using Unity.VisualScripting;
using UnityEngine;
public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left,
        right,
        top,
        bottom
    };

    public DoorType doorType;

    public void OnTriggerEnter2D(Collider2D collisor)
    {
        RoomController.instance.PassouPorta(this);
    }
}
