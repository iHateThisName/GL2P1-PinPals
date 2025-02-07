using TMPro;
using UnityEngine;

public class BumperController : MonoBehaviour {
    [SerializeField] float bounceForce;
    [SerializeField] int points = 100;
    [SerializeField] private BumperAnimationController bumperAnimationController;

    private AudioSource bumperAudioSource;

    void Start() {
        bumperAudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision) {
        Rigidbody otherRB = collision.rigidbody;
        otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        // Play the bumper sound
        bumperAudioSource.Play();

        // Add points to the player
        collision.gameObject.GetComponent<ModelController>()._playerScoreTracker.AddPoints(points);
        
        // Play the animation
        bumperAnimationController.PlayAnimation(collision);
    }
}
