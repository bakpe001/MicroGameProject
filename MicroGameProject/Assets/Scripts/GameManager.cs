using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using static Unity.VisualScripting.Metadata;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Speed of diffrent game objects")]
    public float speed = 15f; //Speed of the obsticles and backgound
    public float Platformspeed = 15f; //Speed of the obsticles and backgound
    public float powerUpSpeed = 15f; //Speed of the obsticles
    public bool gameOver = false;

    [Header("Textures and Models")]
    //Refrences to every model, texture and backgound avaiable in the game
    public Background backgound; //Static Refrence to the backgound element
    public PlayerModel playerModel; //Static Refrence to the playerModel element
    public SkinOptions[] skinOptions; //Static Refrence array to the all the skin options for each playermodel element
    private int currentSkin = 0; //Current Texture selected
    
    [Header("Spawning Settings")]
    //Modifiers of spawnning obsticles
    public float startDelay= 2f; //Delay before the game starts spawning obsticles
    public float repeatDelay = 2f; //Delay between the game spawning obsticles
    public bool spawningOn = true; //Bool to determin if the game is spawning obsticles
    public SpawnManager spawnManager; //Reference to the SpawnManager script
    public PlatformSpawnManager platformSpawnManager; // Reference to platform spawner

    
    public PlayerManager playerManager;

    // References to UI and Score management scripts
    public ScoreManager scoreManager;
    public UIManager uiManager;

    private bool playerIsInvisible = false; // Used to manage player invincibility

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initially show the main menu
        uiManager.ShowMainMenu();
        StartGameFromUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            speed = 0; // Stop game if game is over
            return;
        }

        // Update score, time, and lives from ScoreManager
        scoreManager.UpdateScoreData();
        uiManager.UpdateUI();

        if (Input.GetKeyDown(KeyCode.M)) // Detect when K is pressed
        {
            backgound.ChangeBackgound();
        }
        if (Input.GetKeyDown(KeyCode.K)) // Detect when K is pressed
        {
            playerModel.EnableChild(); // Enable next model
            currentSkin = (currentSkin + 1) % skinOptions.Length;
        }
        if (Input.GetKeyDown(KeyCode.L)) // Detect when K is pressed
        {
            // Move to the next child (looping)
            skinOptions[currentSkin].ChangeTexture(); // Change the texture
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            spawningOn = false;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            spawningOn = true;
        }

    }
    // This method is called when the player hits an obstacle
    public void PlayerHitObstacle()
    {
        if (playerManager.isInvincible) return;

        if (scoreManager.GetLives() > 1)
        {
            scoreManager.DecreaseLife();
            playerManager.MakePlayerInvincible();
        }
        else
        {
            scoreManager.DecreaseLife(); // Vähennä viimeinen elämä
            gameOver = true;
            spawningOn = false;
            playerManager.Death();
            uiManager.UpdateUI();
            uiManager.ShowGameOverUI();
        }
    }

    // This method is used to decrease the life and handle game over
    public void DecreaseLife()
    {
        scoreManager.DecreaseLife(); // Decrease life

        if (scoreManager.GetLives() <= 0)
        {
            gameOver = true; // End the game if no lives left
            uiManager.ShowGameOverUI();
        }
    }
    // Method to start the game when called from UIManager
    public void StartGameFromUI()
    {
        // Hide main menu and start the game
        uiManager.StartGame(); // This will hide the main menu and show game UI
        scoreManager.StartGame(); // Initialize score and start the game
        gameOver = false; // Reset game over state
        spawningOn = true; // Allow spawning
    }

    // Method to pause the game
    public void PauseGame()
    {
        uiManager.PauseGame(); // This will show pause menu and stop the game
        gameOver = true; // Optionally set gameOver true if you want to halt the game when paused
    }

    // Method to resume the game
    public void ResumeGame()
    {
        uiManager.ResumeGame(); // Hide the pause menu and resume the game
        gameOver = false;
    }

}
