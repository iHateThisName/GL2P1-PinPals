using UnityEngine;

public class BalloonControler : MonoBehaviour
{
    public float floatStrenght = 1f;
    public float maxHight = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < maxHight)
        {
            rb.AddForce(Vector3.up * floatStrenght);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
