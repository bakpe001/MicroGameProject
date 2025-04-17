using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Metadata;

public class SkinOptions : MonoBehaviour
{
    public int currentIndex = 0;  // Index to track the current texture
    public Texture[] textures;  // Array of textures

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure there's at least one texture in the array
        if (textures.Length > 0)
        {
            // Set the initial texture
            ChangeTexture(currentIndex);
        }
        else
        {
            Debug.LogWarning("No textures assigned to current character!");
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    //These are the funtions to change textures on the gameobjects with overflow for random or set choise
    public void ChangeTexture()
    {
        Renderer renderer = GetComponent<Renderer>();
        currentIndex = (currentIndex + 1) % textures.Length;
        renderer.material.mainTexture = textures[currentIndex];  // Set the new texture
    }
    public void ChangeTexture(int index)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && textures.Length > index)
        {
            renderer.material.mainTexture = textures[index];  // Set the new texture
        }
        else
        {
            Debug.LogWarning("Invalid texture index.");
        }
    }
}
