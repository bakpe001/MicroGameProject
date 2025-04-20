using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private float timeElapsed = 0f;
    private int score = 0;
    private int lives = 3;
    private bool gameActive = false;

    int remainingLives;

    public void StartGame()
    {
        timeElapsed = 0f;
        score = 0;
        lives = 3;
        gameActive = true;
    }

    public void UpdateScoreData()
    {
        if (!gameActive) return;

        timeElapsed += Time.deltaTime;
        score = Mathf.FloorToInt(timeElapsed * 10); // esim. 10 pistettä per sekunti
    }

    public int GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return timeElapsed;
    }
    public int GetLives()
    {
        return lives;
    }
    public int DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;
        }
        return lives;
    }

    // Lisää tämä metodi ajan muuntamiseen min:sek-muotoon
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
    public bool SpendScore(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            return true;
        }
        return false;
    }

    public void AddLife()
    {
        lives++;
    }
}