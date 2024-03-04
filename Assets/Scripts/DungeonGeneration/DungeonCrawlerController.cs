using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public enum Direction
{
    top = 0,
    left = 1,
    down = 2,
    right = 3
}

public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new()
    {
        {Direction.top, Vector2Int.up},
        {Direction.down, Vector2Int.down},
        {Direction.left, Vector2Int.left},
        {Direction.right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new();

        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(ScriptableObject.CreateInstance<DungeonCrawler>());
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }
}
