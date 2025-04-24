using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawnManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab; // Platform prefab to be spawned
    public float minSpawnX = 30f; // Minimum X position to spawn platform
    public float maxSpawnX = 40f; // Maximum X position to spawn platform
    public float minSpawnY = 2f;  // Minimum Y position
    public float maxSpawnY = 7f;  // Maximum Y position
    public float minVerticalSpacing = 1.5f; // Minimum vertical spacing between platforms
    public float minHorizontalSpacing = 3f; // Minimum horizontal spacing between platforms
    public float maxHorizontalSpacing = 10f; // Maximum horizontal spacing allowed

    [Header("Item Settings")]
    public GameObject[] items; // Array of items that may spawn on platforms
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.4f; // Chance to spawn an item on top of platform

    [Header("Burst Spawn Settings")]
    public float minBurstDelay = 2f; // Min delay between bursts
    public float maxBurstDelay = 5f; // Max delay between bursts
    public int minBurstCount = 1;    // Min number of platforms per burst
    public int maxBurstCount = 3;    // Max number of platforms per burst

    public GameManager gameManager; // Reference to game manager

    private List<Vector2> recentPlatformPositions = new List<Vector2>(); // Track recent platform positions

    void Start() { }

    // Call to start spawning platforms
    public void StartSpawning()
    {
        StartCoroutine(SpawnPlatformBursts());
    }

    // Code to continuously spawn bursts of platforms
    IEnumerator SpawnPlatformBursts()
    {
        yield return new WaitForSeconds(gameManager.startDelay); // Initial delay

        while (!gameManager.gameOver)
        {
            if (gameManager.spawningOn)
            {
                int burstCount = Random.Range(minBurstCount, maxBurstCount + 1);

                for (int i = 0; i < burstCount; i++)
                {
                    bool forceMinY = (i == 0); // First in burst forced to min Y
                    SpawnPlatformWithOptionalItem(forceMinY);
                    yield return new WaitForSeconds(0.3f); // Time between platforms in burst
                }

                float delay = Random.Range(minBurstDelay, maxBurstDelay);
                yield return new WaitForSeconds(delay); // Time after burst
            }
            else
            {
                yield return null; // Wait if not spawning
            }
        }
    }

    // Spawn one platform, optionally placing an item on top
    void SpawnPlatformWithOptionalItem(bool forceMinY = false)
    {
        float xPos, yPos;
        int maxAttempts = 20;
        int attempts = 0;

        // Keep generating until valid position found or attempts run out
        do
        {
            xPos = Random.Range(minSpawnX, maxSpawnX);
            yPos = forceMinY ? minSpawnY : Random.Range(minSpawnY, maxSpawnY);
            attempts++;
        }
        while (!IsPositionValid(xPos, yPos) && attempts < maxAttempts);

        // Add new position to the list of recent platform positions
        Vector2 platformPos2D = new Vector2(xPos, yPos);
        recentPlatformPositions.Add(platformPos2D);
        if (recentPlatformPositions.Count > 10)
            recentPlatformPositions.RemoveAt(0); // Keep list size small

        // Spawn the platform
        Vector3 platformPos = new Vector3(xPos, yPos, 0);
        GameObject platform = Instantiate(platformPrefab, platformPos, Quaternion.identity);

        // Optional item spawn with vertical offset
        if (items.Length > 0 && Random.value <= itemSpawnChance)
        {
            Vector3 itemOffset = new Vector3(0, 1f, 0); // Adjust as needed
            Vector3 itemPos = platformPos + itemOffset;

            int index = Random.Range(0, items.Length);
            Instantiate(items[index], itemPos, Quaternion.identity);
        }
    }

    // Ensures a platform is far enough and not too far from existing ones
    bool IsPositionValid(float x, float y)
    {
        foreach (Vector2 pos in recentPlatformPositions)
        {
            float horizontalDistance = Mathf.Abs(x - pos.x);
            float verticalDistance = Mathf.Abs(y - pos.y);

            if (horizontalDistance < minHorizontalSpacing || horizontalDistance > maxHorizontalSpacing)
                return false;

            if (verticalDistance < minVerticalSpacing)
                return false;
        }
        return true;
    }
}
