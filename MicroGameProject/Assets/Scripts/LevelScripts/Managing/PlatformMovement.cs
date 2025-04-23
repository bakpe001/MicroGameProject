using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private GameManager gameManager;
    private float leftBound = -25f; // Updated to match your latest request

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager == null || gameManager.gameOver) return;

        transform.Translate(Vector3.left * Time.deltaTime * gameManager.Platformspeed);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
