using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstacles; // Assign all obstacle prefabs here

    [Header("Game Manager Reference")]
    public GameManager gameManager; // Set this via inspector or dynamically

    [Header("Spawn Position Settings")]
    public Vector2 spawnYRange = new Vector2(1f, 4f); // Vertical randomness
    public float spawnX = 25f; // X position for obstacle spawn

    [Header("Burst Settings")]
    public int minBurstCount = 1; // Min obstacles in a burst
    public int maxBurstCount = 3; // Max obstacles in a burst
    public float spawnChance = 0.5f; // 50% chance per spawn
    public float delayBetweenSpawns = 0.3f; // Delay between obstacles in same burst

    [Header("Burst Cooldown")]
    public float minBurstDelay = 1f; // Min delay after burst
    public float maxBurstDelay = 3f; // Max delay after burst

    void Start()
    {
        // Begin the burst-spawning loop after initial delay
        StartCoroutine(SpawnObstacleBursts());
    }

    IEnumerator SpawnObstacleBursts()
    {
        // Initial delay before first burst
        yield return new WaitForSeconds(gameManager.startDelay);

        while (!gameManager.gameOver)
        {
            // Check if spawning is allowed (controlled by GameManager)
            if (gameManager.spawningOn)
            {
                // Choose how many obstacles to attempt in this burst
                int burstCount = Random.Range(minBurstCount, maxBurstCount + 1);

                for (int i = 0; i < burstCount; i++)
                {
                    // 50% (or defined %) chance to actually spawn an obstacle
                    if (Random.value <= spawnChance)
                    {
                        SpawnObstacle();
                    }

                    // Wait between spawns in same burst
                    yield return new WaitForSeconds(delayBetweenSpawns);
                }

                // Wait between bursts
                float delay = Random.Range(minBurstDelay, maxBurstDelay);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                // If paused, wait until spawning is turned back on
                yield return null;
            }
        }
    }

    // Spawns a random obstacle prefab at a random height
    void SpawnObstacle()
    {
        int index = Random.Range(0, obstacles.Length);
        float y = Random.Range(spawnYRange.x, spawnYRange.y);
        Vector3 spawnPos = new Vector3(spawnX, y, 0);

        Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
    }
}
