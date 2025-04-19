using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float jumpForce = 18f; // The strength of the jump
    public Vector3 gravityModifier = new Vector3(0, -40.0F, 0); // Custom Gravity
    public bool isOnGround = true; // Checking grounded state for jumps
    public bool isDead = false; // Is the player dead?
    public bool DoubleJump = false; // DoubleJump True?
    private bool canDoubleJump = false; //Track whether we can do the double jump (after first jump)

    public int selectedJumpSound = 0; // Jump sound selection from an array
    public int selectedCrashSound = 0; // Crash sound selection from an array

    public GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip[] jumpSound;
    public AudioClip[] crashSound;
    private AudioSource playerAudio;

    
    

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = DoubleJump; // Reset ability to double jump on landing
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            gameManager.gameOver = true;

            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound[selectedCrashSound], 1.0f);
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
}
