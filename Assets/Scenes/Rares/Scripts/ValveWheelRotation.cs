using UnityEngine;

public class ValveWheelRotation : MonoBehaviour {
    public float rotationSpeed = 100f;
    private float timer = 0;
    int random = 0;
    Vector3 dirVector;
    void Start() {
        PickNewDirection();
    }

    void Update() 
    {
        /*
        timer = timer - Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed * direction * Time.deltaTime);
        Debug.Log(Vector3.forward * rotationSpeed * direction * Time.deltaTime);

        if (timer <= 0) ;
        {
            PickNewDirection();
        }*/

        timer = timer - Time.deltaTime;
        transform.Rotate(dirVector * rotationSpeed * Time.deltaTime);
        Debug.Log($"time: {timer}");
        if (timer <= 0) 
        {
            PickNewDirection();
            timer = 3f;
        }
    }

    void PickNewDirection() 
    {
        random = Random.Range(1, 3);
        if (random == 1) {
            dirVector = Vector3.forward;
        }
        else
        {
            dirVector = -Vector3.forward;
        }

    }
}
