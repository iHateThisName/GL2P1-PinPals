using TMPro;
using UnityEngine;
// Hilmir

public class DisplayStats : MonoBehaviour {
    public int currentPoint { get; private set; } = 0;
    [HideInInspector]
    [SerializeField] private PlayerStats storedPlayerStats;
    int bumperPoints;
    int killPoints;
    int deathPoints;
    int usedPowerPoints;
    int collectedPowerPoints;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _bumperHits;
    [SerializeField] private TMP_Text _killPoints;
    [SerializeField] private TMP_Text _deathPoints;
    [SerializeField] private TMP_Text _powerUpUsedPoints;

    // Verifies it and displays it.
    public void SetPlayerName(string name) {
        _playerName.text = name.ToString();
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


    public void PowerUpsCollected(int points) {
        _powerUpUsedPoints.text = points.ToString();
    }
}
