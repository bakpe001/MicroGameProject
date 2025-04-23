using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    #region References
    [Header("Player Main Data")]
    public float jumpForce = 18f; // The strength of the jump
    public Vector3 gravityModifier = new Vector3(0, -40.0F, 0); // Custom Gravity
    public bool isOnGround = true; // Checking grounded state for jumps
    public bool isDead = false; // Is the player dead?
    public bool DoubleJump = false; // DoubleJump True?
    public bool canDoubleJump = false; // Track whether we can do the double jump (after the first jump)
    public int doubleJumpCount = 0; // Tracks how many Double Jumps are left
    public float doubleJumpForce = 15f;

    [Header("Selected SFX Integers")]
    public int selectedJumpSound = 0; // Jump sound selection from an array
    public int selectedCrashSound = 0; // Crash sound selection from an array

    [Header("References")]
    public GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip[] jumpSound;
    public AudioClip[] crashSound;
    private AudioSource playerAudio;

    // Public function to subscribe listeners to. Really useful for events. -Davoth
    [Header("Player Dead Event")]
    public UnityEvent playerDeath;
    #endregion

    private ScoreManager scoreManager;

    private Renderer[] renderers; // Store all renderers in children

    #region Blink Settings
    public float blinkDuration = 2f; // How long the player should blink (invincible period)
    public float blinkInterval = 0.1f; // How fast the player blinks (interval between on/off)
    private bool isBlinking = false; // Prevent multiple coroutines running at the same time
    private bool isInvincible = false; // Tracks if the player is invincible after taking damage
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        scoreManager = FindObjectOfType<ScoreManager>();

        renderers = GetComponentsInChildren<Renderer>(); // Get all renderers in the player and its children
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                // First jump
                Jump();
                isOnGround = false; // Player is no longer on the ground
                if (DoubleJump)
                {
                    canDoubleJump = true;
                }
            }
            else if (canDoubleJump)
            {
                // Double jump
                Jump();
                canDoubleJump = false;
                Jump();
                doubleJumpCount--;
                canDoubleJump = false;

                if (doubleJumpCount <= 0)
                {
                    DoubleJump = false;
                }
            }
        }
    }

    // Detect ground and obstacle collisions
    private void OnCollisionEnter(Collision collision) => ObstacleCheck(collision);

    private void ObstacleCheck(Collision collision)
    {
        // If collided with the ground, reset jumping state
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = DoubleJump; // Reset double jump ability
            dirtParticle.Play();
        }

        // If collided with an obstacle, handle player death or blink effect
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isDead && !isInvincible) // If player is not dead and is not invincible
            {
                scoreManager.DecreaseLife();

                if (scoreManager.GetLives() <= 0)
                {
                    isDead = true;
                    gameManager.gameOver = true;

                    Debug.Log("Game Over!");
                    playerAnim.SetBool("Death_b", true);
                    explosionParticle.Play();
                    dirtParticle.Stop();
                    playerAudio.PlayOneShot(crashSound[selectedCrashSound], 1.0f);
                    playerDeath?.Invoke(); // Trigger death event
                }
                else
                {
                    Debug.Log("Player hit obstacle, lost one life!");
                    // Start blinking effect and invincibility
                    StartCoroutine(EnableInvincibility());
                }
            }
        }
    }

    // Coroutine to make the player blink (toggle visibility on/off) and enable invincibility
    private IEnumerator EnableInvincibility()
    {
        isInvincible = true; // Player becomes invincible
        StartCoroutine(BlinkPlayer()); // Start blinking effect

        // Wait for the invincibility duration
        yield return new WaitForSeconds(blinkDuration);

        isInvincible = false; // Player is no longer invincible
    }

    // Coroutine to handle blinking effect
    private IEnumerator BlinkPlayer()
    {
        isBlinking = true;
        float timer = 0f;

        // Loop for the blink effect
        while (timer < blinkDuration)
        {
            // Hide the player (disable all renderers)
            foreach (Renderer rend in renderers)
            {
                rend.enabled = false;
            }
            yield return new WaitForSeconds(blinkInterval); // Wait for blink interval

            // Show the player (enable all renderers)
            foreach (Renderer rend in renderers)
            {
                rend.enabled = true;
            }
            yield return new WaitForSeconds(blinkInterval); // Wait for blink interval

            timer += blinkInterval * 2; // Increase the timer by the total duration of each blink (on + off)
        }

        isBlinking = false; // Blink effect is finished
    }

    // Performs the jump action
    private void Jump()
    {
        // If it's a regular jump
        if (isOnGround)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Use regular jump force
        }
        // If it's a double jump
        else if (canDoubleJump)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse); // Use double jump force
            canDoubleJump = false;
        }

        isOnGround = false; // Player is no longer on the ground
        playerAnim.SetTrigger("Jump_trig"); // Trigger jump animation
        dirtParticle.Stop(); // Stop dirt particle effect
        playerAudio.PlayOneShot(jumpSound[selectedJumpSound], 1.0f); // Play jump sound
    }

    // This is useful for resetting the player after a gameover screen or equivalent. -Davoth
    public void ResetPlayer()
    {
        jumpForce = 18f; // Reset the strength of the jump
        isOnGround = true; // Player is grounded for jumping
        isDead = false; // Player is not dead
        DoubleJump = false; // No double jump initially
        canDoubleJump = false; // Track if double jump is available
        doubleJumpCount = 0; // Reset double jump count

        selectedJumpSound = 0; // Reset jump sound selection
        selectedCrashSound = 0; // Reset crash sound selection

        // Reset animation and particle effects
        playerAnim.SetBool("Death_b", false);
        explosionParticle.Stop();
        dirtParticle.Play();
    }
}



