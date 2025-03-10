using UnityEngine;
using UnityEngine.SceneManagement;

public class TempOnOffScript : MonoBehaviour
{
    public Material OnTexture;
    public Material OffTexture;

    private Renderer TemkRender;
    private bool IsActive = true;
    private Collider Collideble;

    //public GameObject scoreManager; // Old scoring system
    [SerializeField] private TargetBankScore _targetScore;
    [SerializeField] int points = 100;
    [SerializeField] private AudioClip hitTargetSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TemkRender = GetComponent<Renderer>();
        TemkRender.material = OnTexture;
        Collideble = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collideble.enabled = false;
        if (IsActive)
        {
            SoundEffectManager.Instance.PlaySoundFXClip(hitTargetSFX, transform, 1f);
            TemkRender.material = OffTexture;
            ModelController modelController = collision.gameObject.GetComponent<ModelController>();
            //modelController.AddPlayerPoints(points);
            modelController.PlayerScoreTracker.AddPoints(points);

            if (_targetScore != null)
            {
                _targetScore.OnScore(modelController);
            }
        }
        else
        {
            TemkRender.material = OnTexture;
        }
    }
}
