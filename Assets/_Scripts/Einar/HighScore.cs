using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : Singleton<HighScore>
{
    public int points;
    public int highScore;
    //public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private string currentLevel;

    void Start()
    {
        // Get the current level's name
        currentLevel = SceneManager.GetActiveScene().name;

        // Load the high score for the current level
        if (PlayerPrefs.HasKey(currentLevel))
        {
            highScore = PlayerPrefs.GetInt(currentLevel);
        }
        else
        {
            highScore = 0; // Initialize high score to 0 if not set
        }

        if (highScoreText != null && PlayerPrefs.HasKey(Helper.level1))
        {
            highScoreText.text = PlayerPrefs.GetInt(Helper.level1).ToString();
        }

        // Initialize scoreGained (Usually to 0 at the start of a level)
        //points = 0; // Set this based on your game logic
    }

    public void CheckHighScore(int currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(currentLevel, highScore);
        }
    }

    //void Update()
    //{
    //    ModelController modelController = GetComponent<ModelController>();
    //    GetComponent<ModelController>().PlayerScoreTracker.AddPoints(points);

    //    //scoreText.text = "Score: " + points;
    //    highScoreText.text = "High Score: " + highScore;

    //    // Check if the current score is greater than the highScore and update
    //    if (points > highScore)
    //    {
    //        highScore = points;
    //        PlayerPrefs.SetInt(currentLevel, highScore); // Save the new high score
    //    }

    //    // Optionally save scoreGained for use elsewhere (Ensure you manage where you want to save it)
    //    PlayerPrefs.SetInt("CurrentScore", points);
    //}

    private void OnDestroy()
    {
        // Optionally save the current score when exiting the level or when the game ends
        PlayerPrefs.SetInt("CurrentScore", points);
    }
}    
// Start is called once before the first execution of Update after the MonoBehaviour is created


    //void Start()
    //{
    //    SceneManager.GetActiveScene().name;
    //    if (PlayerPrefs.HasKey(Helper.level1))
    //    {
    //        highScore = PlayerPrefs.GetInt(Helper.level1);
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //Need to referance the score script
    //    //Need to referance the lvls
    //    if (scoreGained > highScore)
    //    {
    //        highScore = scoreGained;
    //        PlayerPrefs.SetInt(Helper.level1, highScore);
    //    }

    //    scoreText.text = "Score: " + scoreGained;
    //    PlayerPrefs.SetInt("Score", scoreGained);
    //    highScoreText.text = "High Score: " + highScore;
    //}
