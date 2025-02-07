using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public int score;
    public int hiScore;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text hiScoreText;
    public void Start() {
        if (PlayerPrefs.HasKey("HighScore")) {
            hiScore = PlayerPrefs.GetInt("HighScore");
        }

        {

        }
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");

        hiScore = PlayerPrefs.GetInt("HighScore");
        hiScoreText.text = "High Score: " + hiScore;
    }
    public void FixedUpdate() {
        scoreText.text = score + " points!";
        PlayerPrefs.SetInt("Score", score);
        hiScoreText.text = "High Score: " + hiScore;

        if (score > hiScore) {
            hiScore = score;
            PlayerPrefs.SetInt("HighScore", hiScore);
        }
    }
}
