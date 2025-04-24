using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Tooltip("Amount of points to add when picked up")]
    public int scoreAmount = 10;
    
    private ScoreManager scoreManager;

    void Start()
    {
        // find the ScoreManager in scene
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && scoreManager != null)
        {
            Debug.Log("PowerUp1 ker�tty!");
            scoreManager.AddScore(scoreAmount);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("ScoreManager on NULL tai t�rm�ys ei ollut Player!");
        }
    }
}
