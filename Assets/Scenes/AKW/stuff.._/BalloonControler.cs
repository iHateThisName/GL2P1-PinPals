using UnityEngine;

public class BalloonControler : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Debug.Log("Pop!");
    }
}
