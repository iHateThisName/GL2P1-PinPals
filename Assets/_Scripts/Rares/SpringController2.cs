using UnityEngine;

public class Plunger : MonoBehaviour
{
    public Rigidbody ball;
    public float maxPull = 10f; // How far it can move backward
    public float launchForce = 500f; // Force applied to ball
    private Vector3 startPos;
    private float pullAmount = 0f;
    private bool isPulling = false;

    void Start()
    {
        startPos = transform.position - new Vector3(0, 0, 0); // Keeping this for flexibility
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            isPulling = true;
            pullAmount = Mathf.Min(pullAmount + Time.deltaTime * 3, maxPull);
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, 0, -maxPull), pullAmount / maxPull);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            LaunchBall();
            isPulling = false;
            transform.position = startPos;
            pullAmount = 0;
        }
    }

    void LaunchBall()
    {
        if (ball != null)
        {
            ball.AddForce(Vector3.forward * launchForce * (pullAmount / maxPull), ForceMode.Impulse);
        }
    }
}
