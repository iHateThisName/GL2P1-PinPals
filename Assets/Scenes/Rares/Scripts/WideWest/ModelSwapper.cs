using UnityEngine;

public class ModelSwapper : MonoBehaviour
{
    public GameObject idleModelPrefab;
    public GameObject activeModelPrefab;

    [SerializeField] private TargetBankScore _targetScore;
    [SerializeField] int points = 100;

    private GameObject currentModel;
    private Collider collideble;
    public bool isIdle = true;

    void Start()
    {
        currentModel = Instantiate(idleModelPrefab, transform.position, transform.rotation, transform);
        collideble = currentModel.GetComponent<Collider>();

        if (collideble != null)
            collideble.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isIdle) return;

        isIdle = false;
        Vector3 pos = currentModel.transform.position;
        Quaternion rot = currentModel.transform.rotation;
        Destroy(currentModel);
        currentModel = Instantiate(activeModelPrefab, pos, rot, transform);
        collideble = currentModel.GetComponent<Collider>();

        if (collideble != null)
            collideble.isTrigger = false;

        Destroy(currentModel.GetComponent<BoxCollider>());

        PlayerReferences modelController = other.gameObject.GetComponent<PlayerReferences>();
        if (modelController != null)
        {
            modelController.PlayerScoreTracker.AddPoints(points);
            if (_targetScore != null)
                _targetScore.OnScore(modelController);
        }

        Debug.Log("Hee, hee, hee");
    }
}
