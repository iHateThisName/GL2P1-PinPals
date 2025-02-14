using UnityEngine;

public class BumperController : MonoBehaviour {
    [SerializeField] float bounceForce;
    [SerializeField] int points = 100;
    [SerializeField] private BumperAnimationController bumperAnimationController;

    private AudioSource bumperAudioSource;

    void Start() {
        bumperAudioSource = GetComponent<AudioSource>();
        if (bumperAnimationController == null) Debug.LogWarning("Bumper Animation Controller is not set! for " + gameObject.name);
        if (bumperAudioSource == null) Debug.LogWarning("Bumper Audio Source is not set! for " + gameObject.name);
    }
    private void OnCollisionEnter(Collision collision) {
        Rigidbody otherRB = collision.rigidbody;
        otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        // Play the bumper sound
        bumperAudioSource.Play();

        // Add points to the player
        collision.gameObject.GetComponent<ModelController>()._playerScoreTracker.AddPoints(points);

        // Play the animation
        if (bumperAnimationController != null) bumperAnimationController.PlayAnimation(collision);
    }
}
