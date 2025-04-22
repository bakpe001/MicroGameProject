using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI doubleJumpText;
    public PlayerManager playerManager;

    private float timeElapsed = 0f;
    private int score = 0;
    private int lives = 3;
    private int collectedScore = 0;
    private bool gameActive = true;

    void Start()
    {
        timeElapsed = 0f;
        score = 0;
        lives = 3;
    }

    void Update()
    {
        if (!gameActive) return;

        // Päivitä aika ja pisteet
        timeElapsed += Time.deltaTime;

        // Päivitä UI
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            int timeScore = Mathf.FloorToInt(timeElapsed * 10);
            int totalScore = timeScore + collectedScore;
            scoreText.text = "Score: " + totalScore;
        }

        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);
            timeText.text = $"Time: {minutes:D2}:{seconds:D2}";
        }

        if (livesText != null)
            livesText.text = "Lives: " + lives;

        if (doubleJumpText != null)
            doubleJumpText.text = "Double Jumps: " + playerManager.doubleJumpCount;
    }

    public void DecreaseLife()
    {
        if (!gameActive) return;

        lives--;
        if (lives < 0) lives = 0;
    }
    public void AddScore(int amount)
    {
        collectedScore += amount;
        UpdateUI();
    }

    public void ResetStats()
    {
        timeElapsed = 0f;
        score = 0;
        collectedScore = 0;
        lives = 3;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    public void SetGameActive(bool state)
    {
        gameActive = state;
    }

    public bool IsGameActive()
    {
        return gameActive;
    }
}

