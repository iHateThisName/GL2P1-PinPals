using TMPro;
using UnityEngine;

public class BumperController : MonoBehaviour {
    [SerializeField] float bounceForce;
    [SerializeField] int points = 100;
    //public GameObject scoreManager;

    private AudioSource bumperAudioSource;

    void Start() {
        bumperAudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision) {
        Rigidbody otherRB = collision.rigidbody;
        otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        bumperAudioSource.Play();

        //scoreManager.GetComponent<ScoreManager>().score+=100;

        GameObject parent = collision.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        parent.GetComponentInChildren<PlayerScoreTracker>().AddPoints(points);
    }
}
