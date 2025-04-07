using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject _playerStatsUI;

    private void Start() {
        List<(EnumPlayerTag tag, int score)> playerScores = GameManager.Instance.GetPlayersOrderByScoreWithScore();
        if (playerScores.Count <= 0) return;
        for (int i = 0; i < playerScores.Count; i++)
        {
        ModelController playerModelController = GameManager.Instance.GetModelController(playerScores[i].tag);
            GameObject currentUI = Instantiate(_playerStatsUI, this.transform);
            DisplayStats displayStats = currentUI.GetComponent<DisplayStats>();
            PlayerStats currentPlayerStats = playerModelController.PlayerStats;

            displayStats.SetPlayerName("Player " + playerScores[i].tag.ToString().Substring(playerScores[i].tag.ToString().Length - 2));
            displayStats.BumperPoint(currentPlayerStats.bumperPoint);
            displayStats.BumperHits(currentPlayerStats.bumperHits);
            displayStats.PlayerKills(currentPlayerStats.playersKilled);
            displayStats.PlayerDeaths(currentPlayerStats.playerDeaths);
            displayStats.PowerUpsCollected(currentPlayerStats.powersCollected);
            displayStats.PowerUpUsed(currentPlayerStats.powersUsed);
        }

    }
    //private void DisplayPlayerUIs() {
    //    // If there is already a prefab in the scene, with a player tag assigned to it, move on to the next player

    //}
}
