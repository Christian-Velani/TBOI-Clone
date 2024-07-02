using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static float Health { get; set; } = 6;
    public static int MaxHealth { get; set; } = 6;
    public static float MoveSpeed { get; set; } = 5f;
    public static float FireRate { get; set; } = 0.5f;
    public static float BulletSize { get; set; } = 0.05f;
    private bool bootCollected = false;
    private bool screwCollected = false;
    public List<string> collectedNames = new();
    private void Awake()
    {
        Health = 6;
        if (instance == null)
        {
            instance = this;
        }

    }
    public static void DamagePlayer(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            SceneManager.LoadScene(5);
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
}
