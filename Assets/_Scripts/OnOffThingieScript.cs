using UnityEngine;
using UnityEngine.SceneManagement;

public class OnOffThingieScript : MonoBehaviour
{
    public Material OnTexture;
    public Material OffTexture;

    private Renderer ThingRender;
    private bool IsActive = true;
    private Collider Collideble;

    public GameObject scoreManager;
    [SerializeField] private ThreeTargetsScore _targetScore;

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
            scoreManager.GetComponent<ScoreManager>().score += 50;
            _targetScore.OnScore();
        }
        else
        {
            ThingRender.material = OnTexture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
