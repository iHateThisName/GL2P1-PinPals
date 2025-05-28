using UnityEngine;

public class TumbleForceMover : MonoBehaviour
{
    public GameObject TumbleWeed;

    void Update()
    {
        transform.position = TumbleWeed.transform.position;
    }
}
