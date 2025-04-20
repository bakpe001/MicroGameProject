using System.Collections;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private float leftBound = -15;
    private bool stop = false;

    public enum PowerUpType
    {
        Speed = 0,
        Invincibility = 1,
    }

    public PowerUpType powerUpType;
    
    private PlayerManager playerManager;
    private PowerUpManager powerUpManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        powerUpManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.Translate(Vector3.left * Time.deltaTime * powerUpManager.powerUpSpeed);
        }


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
            if (powerUpType == PowerUpType.Speed)
            {
                Debug.Log("Speed Power Up");
                StartCoroutine(powerUpManager.SpeedPowerUp());
            }
            else if (powerUpType == PowerUpType.Invincibility)
            {
                Debug.Log("Invincible");
                StartCoroutine(powerUpManager.InvincibilityPowerUp());
            }
            stop = true;
            GetComponent<MeshRenderer>().enabled = false;
            transform.Translate(Vector3.up * 15);
            StartCoroutine(DestroyPowerUp());
        }
    }

    IEnumerator DestroyPowerUp()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
