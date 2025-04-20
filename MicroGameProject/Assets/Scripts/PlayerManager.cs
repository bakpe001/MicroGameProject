using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    [Header("Jump modifiers")]
    public float jumpForce = 18f; //The strength of the jump
    public Vector3 gravityModifier = new Vector3(0, -40.0F, 0); //Custom Gravity
    public bool isOnGround = true; //Checking grounded state for jumps

    [Header("Player state")]
    public bool isDead = false; //Is the player dead?
    public bool isInvincible = false; //Is the player invincible?
    private bool isInvisible = false; // Is the player invisible?

    [Header("Sound Effect Selection")]
    public int selectedJumpSound = 0; //Jump sound selection from an array
    public int selectedCrashSound = 0; //Crash sound selection from an array

    [Header("Invicible Launch Force")]
    public float invincibleLaunchForce = 3.5f; //The force applied to the player when invincible

    [Header("Data intake")]
    public GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip[] jumpSound;
    public AudioClip[] crashSound;
    private AudioSource playerAudio;

    private Renderer[] renderers;
    public ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity = gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isDead)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collides with ground, set isOnGround to true
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        // If player collides with an obstacle and is NOT invincible
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
            {
                gameManager.PlayerHitObstacle();
            }
        }
        // If player collides with an obstacle while invincible, apply launch force
        if (collision.gameObject.CompareTag("Obstacle") && isInvincible)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 launchDirection = collision.gameObject.transform.position - new Vector3(-2 , -10, 10);

            enemyRigidbody.AddForce(launchDirection * invincibleLaunchForce, ForceMode.Impulse);
            enemyRigidbody.AddTorque(new Vector3(0, 0, 20), ForceMode.Impulse);
        }
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound[selectedJumpSound], 1.0f);
    }
    public void Death()
    {
        isDead = true;
        gameManager.gameOver = true;

        Debug.Log("Game Over!");
        playerAnim.SetBool("Death_b", true);
        explosionParticle.Play();
        dirtParticle.Stop();
        playerAudio.PlayOneShot(crashSound[selectedCrashSound], 1.0f);

        // Handle invincibility when death occurs
        StartCoroutine(HandleInvincibility());
    }
    // Coroutine to handle invincibility and invisibility effect
    private IEnumerator HandleInvincibility()
    {
        isInvincible = true;

        bool skipInvisibility = gameManager.scoreManager.GetLives() <= 1;

        if (!skipInvisibility)
        {
            isInvisible = true;
            SetPlayerVisible(false);
            yield return new WaitForSeconds(2f);
            SetPlayerVisible(true);
            isInvisible = false;
        }
        else
        {
            // Ei näkymättömyyttä jos viimeinen elämä
            yield return new WaitForSeconds(2f);
        }

        isInvincible = false;
    }
    private IEnumerator BlinkEffect()
    {
        for (int i = 0; i < 6; i++)
        {
            SetPlayerVisible(false);
            yield return new WaitForSeconds(0.1f);
            SetPlayerVisible(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void MakePlayerInvincible()
    {
        StartCoroutine(HandleInvincibility());
        StartCoroutine(BlinkEffect());
    }
    private void SetPlayerVisible(bool visible)
    {
        foreach (Renderer rend in renderers)
        {
            rend.enabled = visible;
        }
    }

}
