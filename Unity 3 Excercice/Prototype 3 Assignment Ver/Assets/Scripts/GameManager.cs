using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Metadata;

public class GameManager : MonoBehaviour
{
    public float speed = 15f; //Speed of the obsticles and backgound
    public bool gameOver = false;

    public Background backgound; //Static Refrence to the backgound element
    public PlayerModel playerModel; //Static Refrence to the playerModel element
    public SkinOptions[] skinOptions; //Static Refrence array to the all the skin options for each playermodel element
    private int currentSkin = 0; //Current Texture selected


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            speed = 0;
        }

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
    }

}
