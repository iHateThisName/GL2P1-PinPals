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
            //scoreManager.GetComponent<ScoreManager>().score += 50;
            GameObject parent = collision.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            PlayerScoreTracker playerTracker = parent.GetComponentInChildren<PlayerScoreTracker>();
            playerTracker.AddPoints(points);
            _targetScore.OnScore(playerTracker);
            //New scoring tracker
        }
        else
        {
            ThingRender.material = OnTexture;
        }
    }
}
