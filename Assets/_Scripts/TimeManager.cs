using System;
using System.Collections;
using TMPro;
using UnityEngine;
//Einar

public class TimeManager : Singleton<TimeManager> {
    //A bunch of other variables
    bool stopWatchActive = false;
    float currentTime;

    [SerializeField] public int counter = 120;
    //public TMP_Text currentTimeText;
    public GameObject restartMenu;

    //[SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip countDownSFX;

    private bool hasPlayedCountdown = false;
    public string TimerText = "00:00";

    // Start is called before the first frame update
    public void Start() {


        //stopWatchActive = true;

        currentTime = counter;
        hasPlayedCountdown = false;

}

    public void FixedUpdate() {
        //if (currentTimeText == null) return;
        if (stopWatchActive == true) {

            if (!hasPlayedCountdown && currentTime <= 10f && currentTime > 0f)
            {
                
                SoundEffectManager.Instance.PlaySoundFXClip(countDownSFX, transform, 1f);
                hasPlayedCountdown = true;
            }
            if (currentTime > 0) {
                currentTime -= Time.deltaTime;

            } else {
                //SoundEffectManager.Instance.PlaySoundFXClip(gameOverSFX, transform, 1f);
                stopWatchActive = false;
                restartMenu.SetActive(true);
                Time.timeScale = 0;

                StartCoroutine(DelayLoad());
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        this.TimerText = "Time left: " + time.ToString(@"mm\:ss\.ff");
        //currentTimeText.text = TimerText;
    }

    private IEnumerator DelayLoad() {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        GameManager.Instance.ScoreScene();

    }

    public void StartStopWatch() {
        stopWatchActive = true;

    }

    public void StopStopWatch() {
        stopWatchActive = false;
    }
}
