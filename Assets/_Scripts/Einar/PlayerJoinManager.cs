using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> _levelSpawnPoints = new List<Transform>();

    private void Awake()
    {
        //_spawnPoints.Clear();
        SceneManager.sceneLoaded -= OnNewLevelLoaded; // to prevent duplicate subscriptions when Awake() is called multiple times.
        SceneManager.sceneLoaded += OnNewLevelLoaded; // gets called every time a scene is loaded
        
    }
    public void OnPlayerJoin()
    {

        switch (GameManager.Instance.Players.Count)
        {
            case 1:
                GameManager.Instance.MovePlayer(EnumPlayerTag.Player01, _spawnPoints[0].position);
                break;
            case 2:
                GameManager.Instance.MovePlayer(EnumPlayerTag.Player02, _spawnPoints[1].position);
                break;
            case 3:
                GameManager.Instance.MovePlayer(EnumPlayerTag.Player03, _spawnPoints[2].position);
                break;
            case 4:
                GameManager.Instance.MovePlayer(EnumPlayerTag.Player04, _spawnPoints[3].position);
                break;
        }

    }

    public void Respawn(EnumPlayerTag tag)
    {
        int playerNumber = (int)tag;
        GameManager.Instance.MovePlayer(tag, _spawnPoints[playerNumber - 1].position);
    }
    public void OnNewLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Pro") || scene.name.StartsWith("Level"))
        {
            switch (GameManager.Instance.Players.Count)
            {
                case 1:
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player01, _levelSpawnPoints[0].position);
                    break;
                case 2:
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player01, _levelSpawnPoints[0].position);
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player02, _levelSpawnPoints[1].position);
                    break;
                case 3:
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player01, _levelSpawnPoints[0].position);
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player02, _levelSpawnPoints[1].position);
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player03, _levelSpawnPoints[2].position);
                    break;
                case 4:
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player01, _levelSpawnPoints[0].position);
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player02, _levelSpawnPoints[1].position);
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player03, _levelSpawnPoints[2].position);
                    GameManager.Instance.MovePlayer(EnumPlayerTag.Player04, _levelSpawnPoints[3].position);
                    break;
            }
        } else 
        {
            SceneManager.sceneLoaded -= OnNewLevelLoaded;
        }
    }

}