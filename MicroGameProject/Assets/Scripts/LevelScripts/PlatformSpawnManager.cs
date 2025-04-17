using UnityEngine;
using System.Collections;

public class PlatformSpawnManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameManager gameManager;

    public float spawnXMin = 30f;
    public float spawnXMax = 40f;
    public float spawnYMin = 2f;
    public float spawnYMax = 7f;

    public float minBurstDelay = 3f;
    public float maxBurstDelay = 6f;
    public int platformsPerBurst = 3;
    public float spawnDelay = 0.5f;
    public float spawnChance = 0.4f;

    void Start()
    {
        StartCoroutine(SpawnBurstLoop());
    }

    IEnumerator SpawnBurstLoop()
    {
        while (!gameManager.gameOver)
        {
            for (int i = 0; i < platformsPerBurst; i++)
            {
                if (Random.value <= spawnChance)
                {
                    SpawnPlatform();
                }
                yield return new WaitForSeconds(spawnDelay);
            }

            float delay = Random.Range(minBurstDelay, maxBurstDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnPlatform()
    {
        float x = Random.Range(spawnXMin, spawnXMax);
        float y = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPos = new Vector3(x, y, 0);

        Instantiate(platformPrefab, spawnPos, Quaternion.identity);
    }
}
