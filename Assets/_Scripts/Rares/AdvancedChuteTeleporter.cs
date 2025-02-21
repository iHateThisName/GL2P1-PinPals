using System.Collections;
using UnityEngine;

public class AdvancedChuteTeleporter : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject objectToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Player")) 
        {
            other.transform.position = spawnPoint.position;
            other.GetComponent<Rigidbody>().Sleep();

            
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
                StartCoroutine(Activate());
            }
        }
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSecondsRealtime(3f);
        objectToDisable.SetActive(true);
    }
}
