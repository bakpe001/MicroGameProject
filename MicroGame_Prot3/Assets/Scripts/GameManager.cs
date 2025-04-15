using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameStart = false;
    private SpawnManager spawnManager;
    private PlayerController playerControllerScript;
    public GameObject gameOverScreen;
    public ParticleSystem dirtParticle;
    public Animator playerAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == true)
        {
            GameOverScreen();
        }
    }

    void GameOverScreen()
    {
        gameOverScreen.gameObject.SetActive(true);
    }

    public void Continue()
    {
        playerControllerScript.gameOver = false;
        dirtParticle.Play();
        gameOverScreen.gameObject.SetActive(false);
        playerAnim.SetBool("Death_b", false);
    }
}
