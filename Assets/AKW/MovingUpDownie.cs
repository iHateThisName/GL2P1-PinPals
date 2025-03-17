using UnityEngine;

public class MovingUpDownie : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 10f;
    public bool pauseOnPoints = false;  // Checkbox to enable/disable the pause
    public float pauseDuration = 2f;    // Time to pause at each point (in seconds)

    private bool movingToB = true;
    private bool isPaused = false;
    private float pauseTimer = 0f;

    void Update()
    {
        if (isPaused)
        {
            // Count down the pause timer
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                isPaused = false;  // Resume movement after pause
            }
            return; // Stop further code execution while paused
        }

        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

            if (transform.position == pointB.position)
            {
                movingToB = false;
                if (pauseOnPoints)
                {
                    isPaused = true;
                    pauseTimer = pauseDuration;  // Set the pause timer
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

            if (transform.position == pointA.position)
            {
                movingToB = true;
                if (pauseOnPoints)
                {
                    isPaused = true;
                    pauseTimer = pauseDuration;  // Set the pause timer
                }
            }
        }
    }
}
