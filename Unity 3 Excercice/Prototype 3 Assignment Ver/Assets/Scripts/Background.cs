using UnityEngine;
using UnityEngine.Rendering;

public class Background : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private GameManager gameManager;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }

        transform.Translate(Vector3.left * Time.deltaTime * gameManager.speed);
    }

    //The functions to change backgound to a random one or a given one with overflow
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
    void ChangeBackgound(int backgoundNum)
    {
        if (sprites.Length > 0 && backgoundNum < sprites.Length)
        {
            spriteRenderer.sprite = sprites[backgoundNum];
        }
        else if(backgoundNum >= sprites.Length)
        {
            Debug.Log("Not a valid Backgound choise!");
        }
        else
        {
            Debug.Log("Backgound Sprite Array is empty!");
        }
    }
}
