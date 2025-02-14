using UnityEngine;

public class BalloonControler : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private ConstantForce cForce;
    private Vector3 forceDirection;

    private void Start()
    {
        cForce = GetComponent<ConstantForce>();
        forceDirection = new Vector3(0, 0, 10);
        cForce.force = forceDirection;
    }
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Debug.Log("Pop!");
    }*/
}
