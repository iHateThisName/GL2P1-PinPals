using UnityEngine;

public class SingleSpringController : MonoBehaviour
{
    public float minHeight = 50f; // Minimum compressed height
    public float maxHeight = 100f; // Maximum extended height
    public float springSpeed = 500f; // Speed of spring movement
    private float currentHeight; // Current height of the spring

    void Start()
    {
        // Initialize to the current local Y scale
        currentHeight = transform.localScale.x;
    }

    void Update()
    {
        // Simulate spring behavior (e.g., extend and compress)
        if (Input.GetKey(KeyCode.UpArrow)) // Extend
        {
            currentHeight += Time.deltaTime * springSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) // Retract
        {
            currentHeight -= Time.deltaTime * springSpeed;
        }

        // Clamp the height between the min and max values
        currentHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);

        // Apply the new height by modifying the local scale
        transform.localScale = new Vector3(currentHeight, transform.localScale.y, transform.localScale.z);
    }
}

