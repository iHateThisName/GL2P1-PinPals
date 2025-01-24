using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //A bunch of other variables
    bool stopWatchActive = false;
    float currentTime;

    public int counter;
    public TMP_Text currentTimeText;

    // Start is called before the first frame update
    public void Start()
    {
        counter = 60;

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
