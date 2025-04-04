using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : Singleton<TimeManager> {
    //A bunch of other variables
    bool stopWatchActive = false;
    float currentTime;

    [SerializeField] public int counter = 120;
    public TMP_Text currentTimeText;
    public GameObject restartMenu;

    [SerializeField] private AudioClip gameOverSFX;

    public string TimerText;

    // Start is called before the first frame update
    public void Start() {


        //stopWatchActive = true;

        currentTime = counter;


    }

    public void FixedUpdate() {
        if (stopWatchActive == true) {
            if (currentTime > 0) {
                currentTime -= Time.deltaTime;

            } else {
                SoundEffectManager.Instance.PlaySoundFXClip(gameOverSFX, transform, 1f);
                stopWatchActive = false;
                restartMenu.SetActive(true);
                Time.timeScale = 0;

                StartCoroutine(DelayLoade());
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        this.TimerText = "Time left: " + time.ToString(@"mm\:ss\.ff");
        currentTimeText.text = TimerText;
    }

    private IEnumerator DelayLoade() {
        yield return new WaitForSecondsRealtime(1);
        GameManager.Instance.ScoreScene();
    }

    public void StartStopWatch() {
        stopWatchActive = true;

    }

    public void StopStopWatch() {
        stopWatchActive = false;
    }
}
