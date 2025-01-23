using TMPro;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    [SerializeField] string playerTag;
    [SerializeField] float bounceForce;
    public GameObject scoreManager;

    private AudioSource bumperAudioSource;

    void Start()
    {
        bumperAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == playerTag)
        {
            Rigidbody otherRB = collision.rigidbody;
            otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
            bumperAudioSource.Play();

            scoreManager.GetComponent<ScoreManager>().score++;
        }
    }
}
