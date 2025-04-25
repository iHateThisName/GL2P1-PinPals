using UnityEngine;
using System.Collections;

public class PartiolRotateScript : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y;

    public float rotationAngle = 45f;
    public float rotationSpeed = 2f;
    public bool Pause = false;
    public float pauseDuration = 1f;
    public bool oneWayRotation = false; 

    private float currentAngle = 0f;
    private bool rotatingForward = true;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        float step = rotationSpeed * Time.deltaTime;

        if (oneWayRotation)
        {
            if (rotatingForward)
            {
                currentAngle += step;
                if (currentAngle >= rotationAngle)
                {
                    currentAngle = rotationAngle;
                    rotatingForward = false;
                    if (Pause) StartCoroutine(PauseRotation());
                }
            }
            else
            {
                currentAngle -= step;
                if (currentAngle <= 0f)
                {
                    currentAngle = 0f;
                    rotatingForward = true;
                    if (Pause) StartCoroutine(PauseRotation());
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
                    if (Pause) StartCoroutine(PauseRotation());
                }
            }
            else
            {
                currentAngle -= step;
                if (currentAngle <= -rotationAngle)
                {
                    currentAngle = -rotationAngle;
                    rotatingForward = true;
                    if (Pause) StartCoroutine(PauseRotation());
                }
            }
        }

        ApplyRotation();
    }

    void ApplyRotation()
    {
        Vector3 axis = Vector3.zero;

        switch (rotationAxis)
        {
            case RotationAxis.X:
                axis = Vector3.right;
                break;
            case RotationAxis.Y:
                axis = Vector3.up;
                break;
            case RotationAxis.Z:
                axis = Vector3.forward;
                break;
        }

        transform.localRotation = initialRotation * Quaternion.AngleAxis(currentAngle, axis);
    }

    IEnumerator PauseRotation()
    {
        float tempSpeed = rotationSpeed;
        rotationSpeed = 0;
        yield return new WaitForSeconds(pauseDuration);
        rotationSpeed = tempSpeed;
    }
}