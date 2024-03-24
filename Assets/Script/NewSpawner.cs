using UnityEngine;

public class NewSpawner : MonoBehaviour

{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float sizeX = 1f;
    [SerializeField] private float sizeY = 1f;
    [SerializeField] private float cooldownTimer = 1f;

    private float spawnTime;

    private void Start()
    {
        spawnTime = cooldownTimer;
    }

    private void Update()
    {
        if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }

        if (spawnTime <= 0)
        {
            Spawn();
            spawnTime = cooldownTimer;
        }
    }

    void Spawn()
    {
        float xPos = Random.Range(-sizeX, sizeX) + transform.position.x;
        float yPos = Random.Range(-sizeY, sizeY) + transform.position.y;

        Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}


