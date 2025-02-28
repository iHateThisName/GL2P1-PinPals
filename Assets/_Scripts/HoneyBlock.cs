using UnityEngine;

public class HoneyBlock : MonoBehaviour
{
    public void OnTriggerEnter(Collider player)
    {
        player.GetComponent<Rigidbody>().linearDamping = 5.0f;
    }

    public void OnTriggerExit(Collider player)
    {
        player.GetComponent<Rigidbody>().angularDamping = 0f;
        player.GetComponent<Rigidbody>().linearDamping = 0f;

    }
}
