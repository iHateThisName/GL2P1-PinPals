using UnityEngine;

public class powerupBalloon : MonoBehaviour
{
    public GameObject Balloon;
    public GameObject player;
    public float liftStrength = 5f;   
    public float balloonDistanceAbove = 1.5f;
    void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject == player)
        {
            Vector3 balloonPosition = new Vector3(transform.position.x, transform.position.y + balloonDistanceAbove, transform.position.z);
            GameObject balloon = Instantiate(Balloon, balloonPosition, Quaternion.identity);
                        
            Rigidbody balloonRb = balloon.GetComponent<Rigidbody>();
            if (balloonRb == null)
            {
                balloonRb = balloon.AddComponent<Rigidbody>();
            }

            Rigidbody objectRb = player.GetComponent<Rigidbody>();
            if (objectRb != null)
            {
                objectRb.AddForce(Vector2.up * liftStrength, ForceMode.Force);
            }

            balloonRb.AddForce(Vector2.up * liftStrength, ForceMode.Force);
        }
    }
}
