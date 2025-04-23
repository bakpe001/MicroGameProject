using UnityEngine;
using System.Collections;

public class PlatformSpawnManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab; // Assign your platform prefab in the inspector
    public float minSpawnX = 30f;
    public float maxSpawnX = 40f;
    public float minSpawnY = 2f;
    public float maxSpawnY = 7f;

    [Header("Item Settings")]
    public GameObject[] items; // Array of power-up prefabs
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.4f; // 40% chance to spawn item on platform

    [Header("Burst Spawn Settings")]
    public float minBurstDelay = 2f;
    public float maxBurstDelay = 5f;
    public int minBurstCount = 1;
    public int maxBurstCount = 3;

    public GameManager gameManager;

    void Start()
    {
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnPlatformBursts());
    }

    IEnumerator SpawnPlatformBursts()
    {
        yield return new WaitForSeconds(gameManager.startDelay);

        while (!gameManager.gameOver)
        {
            if (gameManager.spawningOn)
            {
                int burstCount = Random.Range(minBurstCount, maxBurstCount + 1);

                for (int i = 0; i < burstCount; i++)
                {
                    SpawnPlatformWithOptionalItem();
                    yield return new WaitForSeconds(0.3f); // Delay between platforms in a burst
                }

                float delay = Random.Range(minBurstDelay, maxBurstDelay);
                yield return new WaitForSeconds(delay); // Delay after burst
            }
            else
            {
                yield return null;
            }
        }
    }

    void SpawnPlatformWithOptionalItem()
    {
        // Randomize platform position
        float xPos = Random.Range(minSpawnX, maxSpawnX);
        float yPos = Random.Range(minSpawnY, maxSpawnY);
        Vector3 platformPos = new Vector3(xPos, yPos, 0);

        // Spawn the platform
        GameObject platform = Instantiate(platformPrefab, platformPos, Quaternion.identity);

        // 40% chance to spawn item on top
        if (items.Length > 0 && Random.value <= itemSpawnChance)
        {
            // Find center-top of platform (adjust Y as needed based on prefab size)
            Vector3 itemOffset = new Vector3(0, 1f, 0); // 1f above platform — tweak for item height
            Vector3 itemPos = platformPos + itemOffset;

            int index = Random.Range(0, items.Length);
            Instantiate(items[index], itemPos, Quaternion.identity);
        }
    }
}
