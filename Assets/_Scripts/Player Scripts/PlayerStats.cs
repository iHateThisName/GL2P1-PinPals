using UnityEngine;
// Hilmir
public class PlayerStats : MonoBehaviour {
    public int currentPoint { get; private set; } = 0;
    public int bumperPoint { get; private set; } = 0;
    public int bumperHits { get; private set; } = 0;
    public int playersKilled { get; private set; } = 0;
    public int playerDeaths { get; private set; } = 0;
    public int powersCollected { get; private set; } = 0;
    public int powersUsed { get; private set; } = 0;


    public void AddPoint(int points) {
        currentPoint += points;
    }

    public void BumperPoint(int bumpers) {
        this.bumperPoint += bumpers;
    }

    public void BumperHits(int hitBumper) {
        this.bumperHits += hitBumper;
    }

    public void PlayerKills(int kills) {
        this.playersKilled += kills;
    }

    public void PlayerDeaths(int deaths) {
        this.playerDeaths += deaths;
    }

    public void PowerUpsCollected(int powerCollected) {
        this.powersCollected += powerCollected;
    }

    public void PowerUpUsed(int powerUsed) {
        this.powersUsed += powerUsed;
    }
}
