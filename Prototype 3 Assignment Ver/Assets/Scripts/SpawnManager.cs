using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles; //Array of all the possible game obsticles
    public GameManager gameManager; //Reference to the GameManager script
    private Vector3 spawnPos = new Vector3(25, 1, 0);
    public bool spawnReady = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnDelay(gameManager.startDelay)); //Start the spawn delay
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.spawningOn)
        {
            SpawnObsticle();
        }
    }

    void SpawnObsticle()
    {
        if (!gameManager.gameOver && spawnReady)
        {
            //Randomly select an obsticle from the array
            int obsticleIndex = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[obsticleIndex], spawnPos, obstacles[obsticleIndex].transform.rotation);
            spawnReady = false;
            StartCoroutine(SpawnDelay(gameManager.repeatDelay));
        }
    }
    public IEnumerator SpawnDelay(float waitTime) 
    { 
        yield return new WaitForSeconds(waitTime);
        spawnReady = true;
        if (!gameManager.spawningOn)
        {
            spawnReady = false; //Stop spawning obsticles
        }
    }
}
