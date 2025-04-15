using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int difficulty;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        Debug.Log(gameObject.name + " was clicked");
        gameManager.gameStart = true;
        titleScreen.gameObject.SetActive(false);
    }
}