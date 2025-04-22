using UnityEngine;

public class PresureGage : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float targetAngle = 0f;
    public float currentAngle = -70f;
    private bool goingUp = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetNewTargetAngle();
    }

    // Update is called once per frame
    void Update()
    {
        float step = rotationSpeed * Time.deltaTime;
        
        if (goingUp)
        {
            currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, step);
            
            if (Mathf.Approximately(currentAngle, targetAngle))
            {
                goingUp = false;
            }
        }
        else
        {
            currentAngle = Mathf.MoveTowards(currentAngle, -70f, step);
            if(Mathf.Approximately(currentAngle, -70))
            {
                goingUp = true;
                SetNewTargetAngle();
            }
        }
        transform.localRotation = Quaternion.Euler(-90f, currentAngle, 0f);
    }

    void SetNewTargetAngle()
    {
        float randomAmmount = Random.Range(0, 140f);
        targetAngle = -70f + randomAmmount;
        Debug.Log($"New target angle: {targetAngle}");
    }
}
