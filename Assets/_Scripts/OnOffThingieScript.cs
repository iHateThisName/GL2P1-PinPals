using UnityEngine;
using UnityEngine.SceneManagement;

public class OnOffThingieScript : MonoBehaviour
{
    public Material OnTexture;
    public Material OffTexture;

    private Renderer ThingRender;
    private bool IsActive = true;
    private Collider Collideble;

    //public GameObject scoreManager; // Old scoring system
    [SerializeField] private TargetBankScore _targetScore;
    [SerializeField] int points = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ThingRender = GetComponent<Renderer>();
        ThingRender.material = OnTexture;
        Collideble = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collideble.enabled = false;
        if (IsActive)
        {
            ThingRender.material = OffTexture;
            ModelController modelController = collision.gameObject.GetComponent<ModelController>();
            modelController.AddPlayerPoints(points);
            _targetScore.OnScore(modelController);
        }
        else
        {
            ThingRender.material = OnTexture;
        }
    }
}
