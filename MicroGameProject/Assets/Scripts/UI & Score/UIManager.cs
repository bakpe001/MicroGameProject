using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject gameUIPanel;
    public GameObject pauseMenuPanel;
    public GameObject shopPanel;

    // Liitet‰‰n ScoreManager
    private ScoreManager scoreManager;

    void Start()
    {
        // Etsi ScoreManager pelist‰
        scoreManager = FindObjectOfType<ScoreManager>();

        // Varmista, ett‰ peliss‰ on UI n‰kyviss‰ aluksi
        ShowMainMenu();
    }

    // P‰ivitet‰‰n UI:ta pelin aikana (score, aika, el‰m‰t)
    public void UpdateUI()
    {
        // Varmistetaan, ett‰ ScoreManager on lˆydetty
        if (scoreManager != null)
        {
            // P‰ivitet‰‰n pisteet
            scoreText.text = "Score: " + scoreManager.GetScore();

            // P‰ivitet‰‰n aika muodossa min:sek
            timeText.text = "Time: " + scoreManager.GetFormattedTime();

            // P‰ivitet‰‰n el‰m‰t
            livesText.text = "Lives: " + scoreManager.GetLives();
        }
    }

    // N‰yt‰ Game Over -n‰yttˆ
    public void ShowGameOverUI()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    // N‰yt‰ Main Menu (pelin p‰‰valikko)
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true); // N‰ytet‰‰n p‰‰valikko
        gameUIPanel.SetActive(false);  // Piilotetaan pelin UI
        pauseMenuPanel.SetActive(false);  // Piilotetaan taukovalikko
        shopPanel.SetActive(false);  // Piilotetaan kauppan‰kym‰
    }

    // Aloita peli (piilota p‰‰valikko ja n‰yt‰ pelin UI)
    public void StartGame()
    {
        mainMenuPanel.SetActive(false);  // Piilotetaan p‰‰valikko
        gameUIPanel.SetActive(true);  // N‰ytet‰‰n pelin UI
        Time.timeScale = 1f;  // K‰ynnistet‰‰n peli (mik‰li peli oli tauolla)
    }

    // Pys‰yt‰ peli (n‰yt‰ taukovalikko)
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);  // N‰ytet‰‰n taukovalikko
        Time.timeScale = 0f;  // Pys‰ytet‰‰n peli
    }

    // Jatka peli‰ (piilotetaan taukovalikko ja k‰ynnistet‰‰n peli)
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);  // Piilotetaan taukovalikko
        Time.timeScale = 1f;  // Jatketaan peli‰
    }

    // N‰yt‰ kaupan UI
    public void ShowShop()
    {
        mainMenuPanel.SetActive(false);  // Piilotetaan p‰‰valikko
        shopPanel.SetActive(true);  // N‰ytet‰‰n kaupan UI
    }

    // Sulje peli
    public void QuitGame()
    {
        Application.Quit();  // Sulkee pelin
    }
}