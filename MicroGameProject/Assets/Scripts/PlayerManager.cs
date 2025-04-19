using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Header("Jump modifiers")]
    public float jumpForce = 18f; //The strength of the jump
    public Vector3 gravityModifier = new Vector3(0, -40.0F, 0); //Custom Gravity
    public bool isOnGround = true; //Checking grounded state for jumps

    [Header("Player state")]
    public bool isDead = false; //Is the player dead?
    public bool isInvincible = false; //Is the player invincible?

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
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            Death();
        }
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
    private void Death()
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
