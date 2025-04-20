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
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
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
}
