using UnityEngine;
using System.Collections.Generic;

public class MovingUpDownie : MonoBehaviour
{
    public List<Transform> points;  // List to hold all the points (A, B, C, D, etc.)
    public float speed = 10f;
    public bool pauseOnPoints = false;  // Checkbox to enable/disable the pause
    public float pauseDuration = 2f;    // Time to pause at each point (in seconds)

    private int currentPointIndex = 0;  // Index of the current target point
    private bool isPaused = false;
    private float pauseTimer = 0f;

    void Update()
    {
        if (points.Count < 2)  // Ensure there are at least two points
        {
            Debug.LogWarning("At least two points are required!");
            return;
        }

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

        // Move towards the current target point
        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (transform.position == targetPoint.position)
        {
            // We've reached the current target point
            if (pauseOnPoints)
            {
                isPaused = true;
                pauseTimer = pauseDuration;  // Set the pause timer
            }
            else
            {
                // Move to the next point
                currentPointIndex = (currentPointIndex + 1) % points.Count; // Loop back to the start when we reach the last point
            }
        }
    }
}
