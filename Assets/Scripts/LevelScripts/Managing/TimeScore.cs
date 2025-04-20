using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    public GameManager gameManager;
    public TextMeshProUGUI scoreText; // Assign a UI Text in the inspector
    private static float score = 0f; // Made the score static, because there is only one score anyway and it's easier to access now. -Davoth

    void Update()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Guard clause is prettier. -Davoth
        if (!gameManager.countScore) return;

        score += Time.deltaTime;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public static void ResetScore()
    {
        score = 0f;
    }
}
