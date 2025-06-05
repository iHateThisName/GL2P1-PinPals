using UnityEngine;
//an AKW Script
public class OnTriggerMover : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float waitTimeAtPointB = 2f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isInCycle = false;

    private void Start()
    {
        targetPosition = pointA.position;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (targetPosition == pointB.position)
            {
                StartCoroutine(WaitAndReturn());
            }
            else
            {
                isMoving = false;
                isInCycle = false;
            }
        }
    }

    public void ObjectEntered()
    {
        if (isInCycle) return;

        isInCycle = true;
        targetPosition = pointB.position;
        isMoving = true;
    }

    private System.Collections.IEnumerator WaitAndReturn()
    {
        isMoving = false;
        yield return new WaitForSeconds(waitTimeAtPointB);
        targetPosition = pointA.position;
        isMoving = true;
    }
}
