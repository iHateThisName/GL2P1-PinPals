using TMPro;
using UnityEngine;

public class BumperController : MonoBehaviour
{
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
            Rigidbody otherRB = collision.rigidbody;
            otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
            bumperAudioSource.Play();

            scoreManager.GetComponent<ScoreManager>().score+=100;
    }
}
