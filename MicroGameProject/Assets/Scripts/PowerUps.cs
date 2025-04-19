using System.Collections;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerManager playerManager;
    private float leftBound = -15;

    public int powerUpType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * gameManager.powerUpSpeed);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Power up collided with player");
            if (powerUpType == 0)
            {
                gameManager.speed += 5;
                Debug.Log("Speed Power Up");
                StartCoroutine(PowerUpDuration(5, 0));
            }
            else if (powerUpType == 1)
            {
                playerManager.isInvincible = true;
                Debug.Log("Invincible");
                StartCoroutine(PowerUpDuration(5, 1));
            }
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = false; // Disable the renderer to make it invisible
        }
    }

    public IEnumerator PowerUpDuration(float time, int powerUp)
    {
        Debug.Log("Power Up Countdown Started");
        yield return new WaitForSeconds(time);
        if (powerUp == 0)
        {
            gameManager.speed -= 5;
            Debug.Log("Power Up Duration Ended");
        }
        else if (powerUp == 1)
        {
            playerManager.isInvincible = false;
            Debug.Log("Power Up Duration Ended");
        }
        Destroy(gameObject); // Destroy the power-up after the duration
    }

}
