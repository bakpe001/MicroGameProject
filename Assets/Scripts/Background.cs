using UnityEngine;
using UnityEngine.Rendering;

public class Background : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private GameManager gameManager;

    [Header("Background Sprite Settings")]
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite[] sprites; // Array of background sprites to cycle through
    public float switchInterval = 10f; // Time in seconds between automatic background changes

    private float timer = 0f; // Timer for switching backgrounds
    private int currentBackgroundIndex = 0; // Current index of the background sprite

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0]; // Start with the first background
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Looping background logic
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }

        // Background scroll speed
        transform.Translate(Vector3.left * Time.deltaTime * gameManager.speed);

        // Timer for background switching
        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            timer = 0f;
            SwitchToNextBackground();
        }
    }

    // Automatically switch to the next background in the array
    void SwitchToNextBackground()
    {
        if (sprites.Length == 0) return;

        currentBackgroundIndex = (currentBackgroundIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentBackgroundIndex];
    }

    // Public function to change background to a random one
    public void ChangeBackgound()
    {
        if (sprites.Length > 0)
        {
            int randomNumber = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomNumber];
        }
        else
        {
            Debug.Log("Backgound Sprite Array is empty!");
        }
    }

    // Public function to change background to a specific index
    void ChangeBackgound(int backgoundNum)
    {
        if (sprites.Length > 0 && backgoundNum < sprites.Length)
        {
            spriteRenderer.sprite = sprites[backgoundNum];
        }
        else if (backgoundNum >= sprites.Length)
        {
            Debug.Log("Not a valid Backgound choise!");
        }
        else
        {
            Debug.Log("Backgound Sprite Array is empty!");
        }
    }
}
