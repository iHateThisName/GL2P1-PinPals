using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : Singleton<HighScore>
{
    public int points;
    public int highScore;

    public TMP_Text highScoreTextLVL1;
    public TMP_Text highScoreTextLVL2;
    public TMP_Text highScoreTextLVL3;
    public TMP_Text highScoreTextLVL4;
    public TMP_Text highScoreTextLVL5;
    public TMP_Text highScoreTextLVL6;

    private string currentLevel;

    void Start()
    {
        // Get the current level's name
        currentLevel = SceneManager.GetActiveScene().name;

        if (currentLevel != "LevelSelector")
        {
            // Load the high score for the current level
            if (PlayerPrefs.HasKey(currentLevel))
            {
                highScore = PlayerPrefs.GetInt(currentLevel);
            }
            else
            {
                highScore = 0;
            }
        }
        else
        {
            if (highScoreTextLVL1 != null && PlayerPrefs.HasKey(Helper.level1))
            {
                highScoreTextLVL1.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level1).ToString();
            }
            if (highScoreTextLVL2 != null && PlayerPrefs.HasKey(Helper.level2))
            {
                highScoreTextLVL2.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level2).ToString();
            }
            if (highScoreTextLVL3 != null && PlayerPrefs.HasKey(Helper.level3))
            {
                highScoreTextLVL3.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level3).ToString();
            }
            if (highScoreTextLVL4 != null && PlayerPrefs.HasKey(Helper.level4))
            {
                highScoreTextLVL4.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level4).ToString();
            }
            if (highScoreTextLVL5 != null && PlayerPrefs.HasKey(Helper.level5))
            {
                highScoreTextLVL5.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level5).ToString();
            }
            if (highScoreTextLVL6 != null && PlayerPrefs.HasKey(Helper.level6))
            {
                highScoreTextLVL6.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level6).ToString();
            }
        }
    }


    public void CheckHighScore(int currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(currentLevel, highScore);
        }
        if (currentLevel != "LevelSelector")
        {
            if (highScoreTextLVL1 != null && PlayerPrefs.HasKey(Helper.level1))
            {
                highScoreTextLVL1.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level1).ToString();
            }
            if (highScoreTextLVL2 != null && PlayerPrefs.HasKey(Helper.level2))
            {
                highScoreTextLVL2.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level2).ToString();
            }
            if (highScoreTextLVL3 != null && PlayerPrefs.HasKey(Helper.level3))
            {
                highScoreTextLVL3.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level3).ToString();
            }
            if (highScoreTextLVL4 != null && PlayerPrefs.HasKey(Helper.level4))
            {
                highScoreTextLVL4.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level4).ToString();
            }
            if (highScoreTextLVL5 != null && PlayerPrefs.HasKey(Helper.level5))
            {
                highScoreTextLVL5.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level5).ToString();
            }
            if (highScoreTextLVL6 != null && PlayerPrefs.HasKey(Helper.level6))
            {
                highScoreTextLVL6.text = "HIGH SCORE: " + PlayerPrefs.GetInt(Helper.level6).ToString();
            }
        }
    }

    private void OnDestroy()
    {
        // Optionally save the current score when exiting the level or when the game ends
        PlayerPrefs.SetInt("CurrentScore", points);
    }
}
