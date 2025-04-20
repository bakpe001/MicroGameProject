using UnityEngine;

public class GameManager : MonoBehaviour
{
    // REGION THE FUCK UP -Said Davoth calmly.
    #region References
    [Header("Main Game Data")]
    public float speed = 15f; //Speed of the obsticles and backgound
    public float Platformspeed = 15f; //Speed of the obsticles and backgound
    public bool gameOver = false;
    public bool countScore = false; // This is convenient for cases like Menus. -Davoth

    //Refrences to every model, texture and backgound avaiable in the game
    [Header("Scenery Info")]
    public Background backgound; //Static Refrence to the backgound element
    public PlayerModel playerModel; //Static Refrence to the playerModel element
    public SkinOptions[] skinOptions; //Static Refrence array to the all the skin options for each playermodel element
    private int currentSkin = 0; //Current Texture selected

    //Modifiers of spawnning obsticles
    [Header("Spawner Data")]
    public float startDelay= 2f; //Delay before the game starts spawning obsticles
    public float repeatDelay = 2f; //Delay between the game spawning obsticles
    public bool spawningOn = false; //Bool to determin if the game is spawning obsticles
    public SpawnManager spawnManager; //Reference to the SpawnManager script
    public PlatformSpawnManager platformSpawnManager; // Reference to platform spawner
    public PlayerManager playerManager; // Reference to the player manager. -Davoth
    //public TimeScore;

    // UI References
    [Header("UI References")]
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject GameoverUI;
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Guard clauses are prettier. -Davoth
        if (gameOver) speed = 0;

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

    // These are public functions to be called from the UI buttons for example. -Davoth
    #region Public Methods
    // This is called with the Play and Play Again buttons. Reset the game and start it again basically. -Davoth
    public void OnStartGameButton()
    {
        gameOver = false;
        countScore = true;
        spawningOn = true;

        speed = 15f;
        Platformspeed = 15f;

        playerManager.ResetPlayer();

        ScoreManager.ResetScore();
    }

    public void OnPlayerDeath()
    {
        GameUI.SetActive(false);
        GameoverUI.SetActive(true);
    }
    #endregion
}
