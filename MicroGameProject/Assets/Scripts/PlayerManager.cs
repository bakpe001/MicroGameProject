using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public float jumpForce = 18f; //The strength of the jump
    public Vector3 gravityModifier = new Vector3(0, -40.0F, 0); //Custom Gravity
    public bool isOnGround = true; //Checking grounded state for jumps
    public bool isDead = false; //Is the player dead?

    public int selectedJumpSound = 0; //Jump sound selection from an array
    public int selectedCrashSound = 0; //Crash sound selection from an array

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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isDead)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
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

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound[selectedJumpSound], 1.0f);
    }
}
