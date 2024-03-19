using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable] // Permite que esse tipo de struct possa ser serializado
    public struct RandomSpawner
    {
        public SpawnerData spawnerData; // Armazena as informações do que deve ser spawnado
    }

    public GridController grid; // Armazena o grid da sala
    public RandomSpawner[] spawnerData; // Armazena uma lista de spawners

    public void InitializeObjectSpawning()
    {
        foreach (RandomSpawner rs in spawnerData) // Iterando pela lista de spawners
        {
            if (rs.spawnerData.itemToSpawn.GetComponent<EnemyController>()) // Verifica se o item a ser spawnado é um inimigo
            { SpawnObjects(rs); } // Spawna o item

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
