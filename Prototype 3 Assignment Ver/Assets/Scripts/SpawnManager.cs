using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles; //Array of all the possible game obsticles
    private Vector3 spawnPos = new Vector3(25, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Temp way of spawning obsticles
        InvokeRepeating("SpawnObsticle", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObsticle()
    {
        //Temp Spawning solution (doesnt stop on death)
        Instantiate(obstacles[0], spawnPos, obstacles[0].transform.rotation);
    }
}
