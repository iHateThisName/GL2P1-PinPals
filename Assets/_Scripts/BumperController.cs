using UnityEngine;

public class BumperController : MonoBehaviour {
    [SerializeField] float bounceForce;
    [SerializeField] int points = 100;
    [SerializeField] int point = 1;
    [SerializeField] private BumperAnimationController bumperAnimationController;

    private AudioSource bumperAudioSource;

    void Start() {
        bumperAudioSource = GetComponent<AudioSource>();

        if (bumperAudioSource != null) {
            bumperAudioSource.volume = SoundManager.Instance.SFXVolumeWithMasterVolumeApplied();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.tag.StartsWith("Player")) return;

        point++;
        Rigidbody otherRB = collision.rigidbody;
        otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        // Play the bumper sound
        if (bumperAudioSource != null) bumperAudioSource.Play();

        // Add points to the player
        PlayerReferences modelController = collision.gameObject.GetComponent<PlayerReferences>();
        modelController.PlayerScoreTracker.AddPoints(points);

        // Indicate that the player has hit a bumper once.
        modelController.PlayerStats.BumperPoint(points);
        modelController.PlayerStats.BumperHits(point);

        // Play the animation
        if (bumperAnimationController != null) bumperAnimationController.PlayAnimation(collision);
    }
}
