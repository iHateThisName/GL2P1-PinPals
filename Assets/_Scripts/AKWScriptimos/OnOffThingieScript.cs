using UnityEngine;
//an AKW Script
public class OnOffThingieScript : MonoBehaviour {
    public Material OnTexture;
    public Material OffTexture;

    private Renderer ThingRender;
    private bool IsActive = true;
    private Collider Collideble;

    //public GameObject scoreManager; // Old scoring system
    [SerializeField] private TargetBankScore _targetScore;
    [SerializeField] int points = 100;
    [SerializeField] private AudioClip hitTargetSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        ThingRender = GetComponent<Renderer>();
        ThingRender.material = OnTexture;
        Collideble = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision) {
        Collideble.enabled = false;
        if (IsActive) {
            SoundEffectManager.Instance.PlaySoundFXClip(hitTargetSFX, transform, 1f);
            ThingRender.material = OffTexture;
            PlayerReferences modelController = collision.gameObject.GetComponent<PlayerReferences>();
            //modelController.AddPlayerPoints(points);
            modelController.PlayerScoreTracker.AddPoints(points);

            if (_targetScore != null) {
                _targetScore.OnScore(modelController);
            }
        } else {
            ThingRender.material = OnTexture;
        }
    }
}
