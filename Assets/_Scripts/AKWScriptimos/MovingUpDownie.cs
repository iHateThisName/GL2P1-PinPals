using System.Collections.Generic;
using UnityEngine;
//an AKW Script
public class MovingUpDownie : MonoBehaviour
{
    public List<Transform> points;
    public float speed = 10f;
    public bool reverseDirection = false;
    public bool pauseOnPoints = false;
    public float pauseDuration = 1f;
    public bool randomizeTarget = false;


    private int currentPointIndex = 0;
    public bool isPaused = false;
    private float pauseTimer = 15f;

    void Update()
    {
        if (points.Count < 2)
        {
            Debug.LogWarning("Two points are required for smooving");
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

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
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
            else if (reverseDirection)
            {
                if (currentPointIndex == 0)
                {

                    currentPointIndex = points.Count - 1;
                }
                else
                {
                    currentPointIndex--;
                }

            }
            else
            {
                currentPointIndex = (currentPointIndex + 1) % points.Count; 

                //if (currentPointIndex == points.Count - 1)
                //{
                //    currentPointIndex = 0;
                //}
                //else
                //{
                //    currentPointIndex++;
                //}
            }
        }
    }
}
