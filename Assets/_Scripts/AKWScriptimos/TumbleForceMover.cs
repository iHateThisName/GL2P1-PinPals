using UnityEngine;
//an AKW Script
public class TumbleForceMover : MonoBehaviour
{
    public GameObject TumbleWeed;

    void Update()
    {
        transform.position = TumbleWeed.transform.position;
    }
}
