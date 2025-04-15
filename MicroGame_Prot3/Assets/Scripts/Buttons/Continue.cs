using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button.onClick.AddListener(gameManager.Continue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
