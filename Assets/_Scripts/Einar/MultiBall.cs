using UnityEngine;
using System.Threading.Tasks;

public class MultiBall : MonoBehaviour
{
    public GameObject objectToDuplicate;  // Reference to the object you want to duplicate

    public void OnTriggerEnter(Collider player)
    {
        // Check if the objectToDuplicate is assigned
        if (objectToDuplicate != null)
        {
            // Duplicate the object by instantiating it
            GameObject duplicate = Instantiate(objectToDuplicate);

            // Optionally, you can set the position, rotation, and scale of the duplicate
            duplicate.transform.position = objectToDuplicate.transform.position;  // Change position to (2, 0, 0)
            duplicate.transform.rotation = Quaternion.identity;    // Reset rotation to default
        }
    }
}
