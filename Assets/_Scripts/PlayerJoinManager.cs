using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerAnimationController;

// Einar and Ivar
public class PlayerJoinManager : Singleton<PlayerJoinManager> {
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> _levelSpawnPoints = new List<Transform>();

    protected override void Awake() {
        base.Awake();
        SceneManager.sceneLoaded -= OnNewLevelLoaded; // to prevent duplicate subscriptions when Awake() is called multiple times.
        SceneManager.sceneLoaded += OnNewLevelLoaded; // gets called every time a scene is loaded

    }
    public void OnPlayerJoin() {

        switch (GameManager.Instance.Players.Count) {
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

    public IEnumerator RespawnDelay(EnumPlayerTag tag, EnumPlayerAnimation playerAnimation) {
        //Play animation

        PlayerAnimationController animationController = GameManager.Instance.GetPlayerReferences(tag).PlayerAnimationController;

        yield return StartCoroutine(animationController.PlayAnimation(playerAnimation));
        Respawn(tag);
    }

    public void Respawn(EnumPlayerTag tag) {
        int playerNumber = (int)tag;
        PlayerPowerController power = GameManager.Instance.GetPlayerReferences(tag).PlayerPowerController;
        power.PlayerRespawns();
        GameManager.Instance.MovePlayer(tag, _spawnPoints[playerNumber - 1].position);
    }
    public void OnNewLevelLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name.StartsWith("Pro") || scene.name.StartsWith("Level")) {
            switch (GameManager.Instance.Players.Count) {
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
        } else {
            SceneManager.sceneLoaded -= OnNewLevelLoaded;
        }
    }

}