using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    #region References
    [Header("Player Main Data")]
    public float jumpForce = 18f; // The strength of the jump
    public Vector3 gravityModifier = new Vector3(0, -40.0F, 0); // Custom Gravity
    public bool isOnGround = true; // Checking grounded state for jumps
    public bool isDead = false; // Is the player dead?
    public bool DoubleJump = false; // DoubleJump True?
    private bool canDoubleJump = false; //Track whether we can do the double jump (after first jump)

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

    // Public function to subscribe listeners to. Really fucking handy. -Davoth
    [Header("Player Dead Event")]
    public UnityEvent playerDeath;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity = gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
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
                canDoubleJump = DoubleJump; // Enable 2nd jump if powerup active
            }
            else if (DoubleJump && canDoubleJump)
            {
                // Second jump
                Jump();
                canDoubleJump = false; // Consume double jump
            }
        }
    }

    // Detect ground and obstacle collisions
    private void OnCollisionEnter(Collision collision) => ObstacleCheck(collision);

    private void ObstacleCheck(Collision collision)
    {
        // Wasn't deadly, so reset the jump.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = DoubleJump; // Reset ability to double jump on landing
            dirtParticle.Play();
        }

        // Holy shit a barrel, lethal as fuck. Gameover.
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            gameManager.gameOver = true;

            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound[selectedCrashSound], 1.0f);

            // Player died, invoke the public event and run all subscribed code. -Davoth
            playerDeath?.Invoke();
        }
    }

    // Performs the jump action
    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // player jump force
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound[selectedJumpSound], 1.0f); // Play jump sound
    }

    // This is useful for resetting the player after a gameover screen or equivalent. -Davoth
    public void ResetPlayer()
    {
        jumpForce = 18f; // The strength of the jump
        isOnGround = true; // Checking grounded state for jumps
        isDead = false; // Is the player dead?
        DoubleJump = false; // DoubleJump True?
        canDoubleJump = false; //Track whether we can do the double jump (after first jump)

        selectedJumpSound = 0; // Jump sound selection from an array
        selectedCrashSound = 0; // Crash sound selection from an array

        // Reset animation and particle FX. -Davoth
        playerAnim.SetBool("Death_b", false);
        explosionParticle.Stop();
        dirtParticle.Play();
    }
}
