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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoomController.instance.PassouPorta(this);
        }
    }
}
