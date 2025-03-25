using UnityEngine;
using System.Collections;

public class PartiolRotateScript : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y; 

    public float rotationAngle = 45f; 
    public float rotationSpeed = 2f; 
    public bool enablePause = true;
    public float pauseDuration = 1f; 

    private float currentAngle = 0f;
    private bool rotatingForward = true;

    void Update()
    {
        float step = rotationSpeed * Time.deltaTime;

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
        float tempSpeed = rotationSpeed;
        rotationSpeed = 0;
        yield return new WaitForSeconds(pauseDuration);
        rotationSpeed = tempSpeed;
    }
}
