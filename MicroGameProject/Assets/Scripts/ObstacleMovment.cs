using UnityEngine;

public class ObstacleMovment : MonoBehaviour
{
    private GameManager gameManager;
    private float leftBound = -15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * gameManager.speed);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < -10  )
        {
            Destroy(gameObject);
        }
    }

}
