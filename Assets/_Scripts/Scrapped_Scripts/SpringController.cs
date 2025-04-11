using UnityEngine;

public class SpringController : MonoBehaviour
{

    public Transform PinBall; // Inser ball here
    public float TheForce = 1000f; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody PinBallRigidBody = PinBall.GetComponent<Rigidbody>();
            PinBallRigidBody.AddForce(Vector3.up * TheForce); // Change the direction
        }
    }
}
