using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //A bunch of other variables
    bool stopWatchActive = false;
    float currentTime;

    [SerializeField] public int counter = 120;
    public TMP_Text currentTimeText;
    public GameObject restartMenu;

    // Start is called before the first frame update
    public void Start()
    {
     

        stopWatchActive = true;

        currentTime = counter;


    }

    public void FixedUpdate()
    {
        if (stopWatchActive == true)
        {
            if (currentTime > 0) 
            {
                currentTime -= Time.deltaTime;

            }
            else
            {
                stopWatchActive = false;
                restartMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Time left: " + time.ToString(@"mm\:ss\.ff");
    }

    public void StartStopWatch()
    {
        stopWatchActive = true;

    }

    public void StopStopWatch()
    {
        stopWatchActive = false;
    }
}
