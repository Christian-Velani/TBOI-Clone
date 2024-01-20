using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static float Health { get; set; } = 6;
    public static int MaxHealth { get; set; } = 6;
    public static float MoveSpeed { get; set; } = 5f;
    public static float FireRate { get; set; } = 0.5f;
    public static float BulletSize { get; set; } = 0.5f;
    private bool bootCollected = false;
    private bool screwCollected = false;
    public List<string> collectedNames = new();
    public Text healthText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + Health;
    }

    public static void DamagePlayer(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        Health = Mathf.Min(MaxHealth, Health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        MoveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        FireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        BulletSize += size;
    }

    public void UpdateCollectedItems(CollectionController item)
    {
        collectedNames.Add(item.item.name);

        foreach (string itemName in collectedNames)
        {
            switch (itemName)
            {
                case "Boot":
                    bootCollected = true;
                    break;
                case "Screw":
                    screwCollected = true;
                    break;
            }
        }

        if (bootCollected && screwCollected)
        {
            FireRateChange(0.25f);
        }
    }

    private static void KillPlayer()
    {

    }
}
