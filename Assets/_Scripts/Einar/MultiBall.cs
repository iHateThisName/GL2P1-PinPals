using UnityEngine;
using System.Threading.Tasks;

public class MultiBall : MonoBehaviour
{
  // Reference to the object you want to duplicate

    public void OnTriggerEnter(Collider player)
    {
        // Check if the objectToDuplicate is assigned
        if (player != null)
        {
            CreateDuplicate(player);
            CreateDuplicate(player);
            Destroy(gameObject);
        }
    }

    private static void CreateDuplicate(Collider player)
    {
        // Duplicate the object by instantiating it
        GameObject duplicate = Instantiate(player.gameObject);

        Debug.Log(player.gameObject.name);
        // Optionally, you can set the position, rotation, and scale of the duplicate
        duplicate.transform.position = player.transform.position;  // Change position to (2, 0, 0)
        duplicate.transform.rotation = Quaternion.identity;    // Reset rotation to default
    }
}
