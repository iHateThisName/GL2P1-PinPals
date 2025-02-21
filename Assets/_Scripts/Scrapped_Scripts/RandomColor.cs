using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Renderer _renderer;
    public List<Color> colors = new List<Color>
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow
    };

    private int currentColorIndex = 0; 
    
    public void Start()
    {
        string playerTag = player.tag;
        Debug.Log(playerTag);
        int playerNumber = int.Parse(playerTag.Substring(playerTag.Length - 1));

        if (_renderer != null)
        {
            _renderer.material.color = colors[playerNumber-1];
        }
        else
        {
            Debug.LogError("No Renderer found on the spawned object!");
        }
    }
}