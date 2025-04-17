using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public int currentIndex = 0;
    public GameObject[] children;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAllChildren();
        EnableChild(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {


    }

    //These are the funtions to change models of the gameobjects with overflow for random or set choise
    public void EnableChild()
    {
        currentIndex = (currentIndex + 1) % children.Length;  // Move to the next child (looping)
        DisableAllChildren(); // Disable all children first
        children[currentIndex].SetActive(true); // Enable the selected child
    }
    public void EnableChild(int index)
    {
        if (index >= 0 && index < children.Length)
        {
            DisableAllChildren(); // Disable all children first
            children[index].SetActive(true); // Enable the selected child
        }
        else
        {
            Debug.LogWarning("Not a valid index child of the player");
        }
    }
    // Disable all children
    private void DisableAllChildren()
    {
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
    }
}
