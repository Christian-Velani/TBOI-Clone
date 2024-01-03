using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    new Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        collectedText.text = "Items Collected: " + collectedAmount;
    }
}
