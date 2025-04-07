using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    // Hilmir
    public int currentPoint { get; private set; } = 0;
    [HideInInspector]
    [SerializeField] private PlayerStats storedPlayerStats;
    int bumperPoints;
    int killPoints;
    int deathPoints;
    int usedPowerPoints;
    int collectedPowerPoints;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _bumperPoints;
    [SerializeField] private TMP_Text _bumperHits;
    [SerializeField] private TMP_Text _killPoints;
    [SerializeField] private TMP_Text _deathPoints;
    [SerializeField] private TMP_Text _powerUpUsedPoints;
    [SerializeField] private TMP_Text _powerUpCollectedPoints;

    private void Start() {



        //// For Player 01
        //ModelController playerModelController = GameManager.Instance.GetModelController(playerScores[0].tag);

        //PlayerStats currentPlayerStats = playerModelController.PlayerStats;
        //// Passes the points to BumperPoint
        //BumperPoint(currentPlayerStats.bumperPoint);
        //BumperHits(currentPlayerStats.bumperHits);
        //PlayerKills(currentPlayerStats.playersKilled);
        //PlayerDeaths(currentPlayerStats.playerDeaths);
        //PowerUpsCollected(currentPlayerStats.powersCollected);
        //PowerUpUsed(currentPlayerStats.powersUsed);

    }
    // Verifies it and displays it.
    public void SetPlayerName(string name) {
        _playerName.text = name.ToString();
    }
    public void BumperPoint(int points) {
        _bumperPoints.text = points.ToString();
    }
    public void BumperHits(int points) {
        _bumperHits.text = points.ToString();
    }

    public void PlayerKills(int points) {
        _killPoints.text = points.ToString();
    }

    public void PlayerDeaths(int points) {
        _deathPoints.text = points.ToString();
    }

    public void PowerUpUsed(int points) {
        _powerUpCollectedPoints.text = points.ToString();
    }

    public void PowerUpsCollected(int points) {
        _powerUpUsedPoints.text = points.ToString();
    }
}
