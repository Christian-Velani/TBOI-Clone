using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    public GameObject heartContainer;
    private float fillValue;

    // Update is called once per frame
    void Update()
    {
        fillValue = GameController.Health;
        fillValue /= GameController.MaxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
