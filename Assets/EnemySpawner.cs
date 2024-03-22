using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int maxSpawnNumber = 5;
    public int currentSpawned = 0;
    private float counter = 0;
    
    public void Update()
    {
        counter += Time.deltaTime;
        int randomPos = Random.Range(0, spawnPoints.Length);
        // 10 means 10 seconds
        if (counter > 5 && currentSpawned < maxSpawnNumber)
        {
            counter = 0;
            currentSpawned++;
            Instantiate(enemyPrefab, spawnPoints[randomPos].position, new Quaternion());
        }
    }

}
