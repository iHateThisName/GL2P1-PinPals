using UnityEngine;

public class MaggotScript : MonoBehaviour
{
    public Material OnTexture;
    public Material OffTexture;

    private Renderer ThingRender;
    private bool IsActive = true;
    private Collider Collideble;

    //public GameObject scoreManager; // Old scoring system
    [SerializeField] private TargetBankScore _targetScore;
    [SerializeField] int points = 100;
    [SerializeField] private AudioClip splatSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ThingRender = GetComponent<Renderer>();
        //ThingRender.material = OnTexture;
        //Collideble = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
            VFXManager.Instance.SpawnVFX(VFXType.Splat, transform.position);
            SoundEffectManager.Instance.PlaySoundFXClip(splatSFX, transform, 1f);
            //ThingRender.material = OffTexture;
            PlayerReferences modelController = collision.gameObject.GetComponent<PlayerReferences>();
            //modelController.AddPlayerPoints(points);
            modelController.PlayerScoreTracker.AddPoints(points);
        Destroy(gameObject);
        
        //Collideble.enabled = false;
        //if (IsActive)
        //{

        //    if (_targetScore != null)
        //    {
        //        _targetScore.OnScore(modelController);
        //    }
        //}
        //else
        //{
        //    ThingRender.material = OnTexture;
        //}
    }
}
