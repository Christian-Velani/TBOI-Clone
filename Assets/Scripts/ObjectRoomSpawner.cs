using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
        public bool spawned;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;

    public void InitializeObjectSpawning()
    {
        foreach (RandomSpawner rs in spawnerData)
        {
            if (rs.spawnerData.itemToSpawn.GetComponent<EnemyController>())
            { SpawnObjects(rs); }

        }
    }

    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform);
            grid.availablePoints.RemoveAt(randomPos);
        }
    }
}
