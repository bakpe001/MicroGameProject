using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstacles; // All available obstacle prefabs to spawn

    [Header("Game Manager Reference")]
    public GameManager gameManager; // Reference to GameManager for game state and speed

    [Header("Spawn Position Settings")]
    public Vector2 spawnYRange = new Vector2(1f, 4f); // Y-axis range for obstacle spawn height
    public float spawnX = 25f; // X-axis position for obstacle spawn (off-screen to the right)

    [Header("Burst Settings")]
    public int minBurstCount = 1; // Minimum number of obstacles per burst
    public int maxBurstCount = 3; // Maximum number of obstacles per burst (this increases over time)
    public float spawnChance = 0.5f; // Chance (0–1) of each spawn attempt actually creating an obstacle
    public float delayBetweenSpawns = 0.3f; // Delay between obstacles in a single burst

    [Header("Burst Cooldown")]
    public float minBurstDelay = 1f; // Minimum time delay between bursts
    public float maxBurstDelay = 3f; // Maximum time delay between bursts (this decreases over time)
    private int maxBurstDelayReductionCount = 0; // Keeps track of how many times the maxBurstDelay has been reduced (max 2)

    void Start()
    {
        // Start the obstacle spawning loop
        StartCoroutine(SpawnObstacleBursts());

        // Start the difficulty scaling over time
        StartCoroutine(IncreaseDifficultyOverTime());
    }

    // Coroutine to spawn bursts of obstacles
    IEnumerator SpawnObstacleBursts()
    {
        // Wait before starting the first burst
        yield return new WaitForSeconds(gameManager.startDelay);

        // Continue while the game is not over
        while (!gameManager.gameOver)
        {
            if (gameManager.spawningOn)
            {
                // Randomly decide how many obstacles to spawn in this burst
                int burstCount = Random.Range(minBurstCount, maxBurstCount + 1);

                for (int i = 0; i < burstCount; i++)
                {
                    // Randomly decide whether to spawn this obstacle
                    if (Random.value <= spawnChance)
                    {
                        SpawnObstacle();
                    }

                    // Wait a bit before trying the next obstacle in the burst
                    yield return new WaitForSeconds(delayBetweenSpawns);
                }

                // Wait a random time before the next burst
                float delay = Random.Range(minBurstDelay, maxBurstDelay);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                // If spawning is off (paused), wait for next frame
                yield return null;
            }
        }
    }

    // Instantiates a single obstacle at a random height
    void SpawnObstacle()
    {
        int index = Random.Range(0, obstacles.Length); // Choose a random obstacle
        float y = Random.Range(spawnYRange.x, spawnYRange.y); // Choose a random Y height
        Vector3 spawnPos = new Vector3(spawnX, y, 0); // Final spawn position

        Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
    }

    // Coroutine that increases difficulty over time
    IEnumerator IncreaseDifficultyOverTime()
    {
        while (!gameManager.gameOver)
        {
            yield return new WaitForSeconds(60f); // Every 60 seconds

            maxBurstCount += 1; // Increase the max number of obstacles per burst

            // Reduce maxBurstDelay up to 2 times total
            if (maxBurstDelayReductionCount < 2)
            {
                maxBurstDelay = Mathf.Max(minBurstDelay, maxBurstDelay - 1f);
                maxBurstDelayReductionCount++;
            }

            // Debug to confirm scaling in console
            Debug.Log($"[Difficulty Up] maxBurstCount: {maxBurstCount}, maxBurstDelay: {maxBurstDelay}");
        }
    }
}
