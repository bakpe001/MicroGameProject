using UnityEngine;
using System.Collections;
using UnityEditor;

public class PowerUpManager : MonoBehaviour
{
    [Header("Power Up Item Settings")]
    public GameObject[] powerUps; // Assign all powerup prefabs here
    public float powerUpSpeed = 15f; // Speed of the power-up item

    [Header("Spawn Position Settings")]
    public Vector2 spawnYRange = new Vector2(1f, 4f); // Vertical randomness
    public float spawnX = 25f; // X position for obstacle spawn

    [Header("Power Up Effect Settings")]
    public float speedIncrease = 15f; // Speed increase for the speed power-up
    public float speedDuration = 5f; // Duration for the speed power-up
    public float invincibilityDuration = 5f; // Duration for the invincibility power-up

    private GameManager gameManager;
    private PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpeedPowerUp()
    {
        gameManager.speed += speedIncrease;
        yield return new WaitForSeconds(speedDuration);
        gameManager.speed -= speedIncrease;
        Debug.Log("Power Up Duration Ended");
    }
    public IEnumerator InvincibilityPowerUp()
    {
        playerManager.isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        playerManager.isInvincible = false;
        Debug.Log("Power Up Duration Ended");
    }

    public void SpawnPowerUp()
    {
        int index = Random.Range(0, powerUps.Length);
        float y = Random.Range(spawnYRange.x, spawnYRange.y);
        Vector3 spawnPos = new Vector3(spawnX, y, 0);

        Instantiate(powerUps[index], spawnPos, powerUps[index].transform.rotation);
    }
    public void SpawnPowerUp(int choise)
    {
        float y = Random.Range(spawnYRange.x, spawnYRange.y);
        Vector3 spawnPos = new Vector3(spawnX, y, 0);

        Instantiate(powerUps[choise], spawnPos, powerUps[choise].transform.rotation);
    }
}
