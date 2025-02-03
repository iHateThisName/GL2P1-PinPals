using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public List<Color> colors;

    public void Start()
    {
        int randomInt = Random.Range(-1, colors.Count);
        if (_renderer != null)
        {
            //_renderer.material.color = colors[randomInt]; // Assigning from the list
            _renderer.material.color = new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)); // Assigning from the list

        }
        else
        {
            Debug.LogError("No Renderer found on the spawned object!");
        }
    }
}

