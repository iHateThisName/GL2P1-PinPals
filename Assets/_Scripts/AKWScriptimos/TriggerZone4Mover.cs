using UnityEngine;
//an AKW Script
public class TriggerZone4Mover : MonoBehaviour
{
    public OnTriggerMover mover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            mover.ObjectEntered();
        }
    }      
}
