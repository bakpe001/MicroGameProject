using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    
    public GameManager gameManager;
    public TextMeshProUGUI scoreText; // Assign a UI Text in the inspector
    private float score = 0f;

    void Update()
    {
        if (!gameManager.gameOver)
        {
            score += Time.deltaTime;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
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
}
