using UnityEngine;
using System.Collections.Generic;

public class MovingUpDownie : MonoBehaviour
{
    public List<Transform> points;  
    public float speed = 10f;
    public bool pauseOnPoints = false;  
    public float pauseDuration = 1f;    
    public bool randomizeTarget = false; 

    private int currentPointIndex = 0;  
    private bool isPaused = false;
    private float pauseTimer = 0f;

    void Update()
    {
        if (points.Count < 2)  
        {
            Debug.LogWarning("Two points are required! for smooving");
            return;
        }

        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                isPaused = false; 
            }
            return; 
        }

        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (transform.position == targetPoint.position)
        {
            if (pauseOnPoints)
            {
                isPaused = true;
                pauseTimer = pauseDuration; 
            }

            if (randomizeTarget)
            {
                int newIndex;
                do
                {
                    newIndex = Random.Range(0, points.Count);
                } while (newIndex == currentPointIndex); 
                currentPointIndex = newIndex;
            }
            else
            {
                currentPointIndex = (currentPointIndex + 1) % points.Count; 
            }
        }
    }
}
