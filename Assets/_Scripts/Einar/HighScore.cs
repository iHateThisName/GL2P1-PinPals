using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public int scoreGained;
    public int highScore;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreGained > highScore)
        {
            highScore = scoreGained;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreText.text = "Missiles destroyed: " + scoreGained;
        PlayerPrefs.SetInt("Score", scoreGained);
        highScoreText.text = "High Score: " + highScore;
    }
}
