using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class SkinManager : MonoBehaviour
{
    public List<Material> skins = new List<Material>();
    private int _selectedSkin = 0;
    [SerializeField] private GameObject playerSkin;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private GameObject skinSelectCanvas;
    public bool _ready;

    public void NextOption()
    {
        _selectedSkin = _selectedSkin + 1;
        if (_selectedSkin == skins.Count)
        {
            _selectedSkin = 0;
        }
        _renderer.material = skins[_selectedSkin];
    }

    public void PreviosOption()
    {
        _selectedSkin = _selectedSkin - 1;
        if (_selectedSkin < 0)
        {
            _selectedSkin = skins.Count -1;
        }
        _renderer.material = skins[_selectedSkin];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartButton()
    {
        _ready = true;
        skinSelectCanvas.SetActive(false);
        //SceneManager.LoadScene("Prototype");

        CheckPlayerReady();

    }

private void CheckPlayerReady()
    {
        int playersReady = 0;

        //GameManager.Instance.Players.Keys = List of EnumPlayerTag
        foreach (EnumPlayerTag tag in GameManager.Instance.Players.Keys)
        {
            ModelController modelController = GameManager.Instance.GetModelController(tag);
            if (modelController.SkinManager._ready)
            {
                playersReady++;
            }
            else
            {
                return;
            }
        }

        if (playersReady == GameManager.Instance.Players.Count)
        {
            //All players ready
            SceneManager.LoadScene("Prototype");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
