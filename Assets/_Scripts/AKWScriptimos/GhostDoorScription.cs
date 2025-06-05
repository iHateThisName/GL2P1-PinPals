using UnityEngine;
using System.Collections;
//an AKW Script
public class GhostDoorScription : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y;

    public float rotationAngle = 45f;
    public float forwardSpeed = 2f;  
    public float backwardSpeed = 1f; 
    public bool enablePause = true;
    public float pauseDuration = 1f;
    public bool oneWayRotation = false; 

    private float currentAngle = 0f;
    private bool rotatingForward = true;

    void Update()
    {
        float step = (rotatingForward ? forwardSpeed : backwardSpeed) * Time.deltaTime;

        if (oneWayRotation)
        {
            if (rotatingForward)
            {
                currentAngle += step;
                if (currentAngle >= rotationAngle)
                {
                    currentAngle = rotationAngle;
                    rotatingForward = false;
                    if (enablePause) StartCoroutine(PauseRotation());
                }
            }
            else
            {
                currentAngle -= step;
                if (currentAngle <= 0f)
                {
                    currentAngle = 0f;
                    rotatingForward = true;
                    if (enablePause) StartCoroutine(PauseRotation());
                }
            }
        }
        else
        {
            if (rotatingForward)
            {
                currentAngle += step;
                if (currentAngle >= rotationAngle)
                {
                    currentAngle = rotationAngle;
                    rotatingForward = false;
                    if (enablePause) StartCoroutine(PauseRotation());
                }
            }
            else
            {
                currentAngle -= step;
                if (currentAngle <= -rotationAngle)
                {
                    currentAngle = -rotationAngle;
                    rotatingForward = true;
                    if (enablePause) StartCoroutine(PauseRotation());
                }
            }
        }

        ApplyRotation();
    }

    void ApplyRotation()
    {
        Vector3 rotation = Vector3.zero;

        switch (rotationAxis)
        {
            case RotationAxis.X:
                rotation = new Vector3(currentAngle, 0, 0);
                break;
            case RotationAxis.Y:
                rotation = new Vector3(0, currentAngle, 0);
                break;
            case RotationAxis.Z:
                rotation = new Vector3(0, 0, currentAngle);
                break;
        }

        transform.localRotation = Quaternion.Euler(rotation);
    }

    IEnumerator PauseRotation()
    {
        float tempForwardSpeed = forwardSpeed;
        float tempBackwardSpeed = backwardSpeed;

        forwardSpeed = 0;
        backwardSpeed = 0;

        yield return new WaitForSeconds(pauseDuration);

        forwardSpeed = tempForwardSpeed;
        backwardSpeed = tempBackwardSpeed;
    }
}
