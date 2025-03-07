using UnityEngine;

public class activatorKeyCode : MonoBehaviour {
    public Material idleTexture;
    public Material activeTexture;

    private Renderer AKRender;
    private Collider Collideble;
    public bool IsIdle = true;

    [SerializeField] private TargetBankScore _targetScore;
    [SerializeField] int points = 100;

    void Start() {
        AKRender = GetComponent<Renderer>();
        AKRender.material = idleTexture;
        Collideble = GetComponent<Collider>();

        Collideble.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (IsIdle) {
            AKRender.material = activeTexture;
            ModelController modelController = other.gameObject.GetComponent<ModelController>();

            IsIdle = false;
            Collideble.isTrigger = false;
            Destroy(GetComponent<BoxCollider>());

            Debug.Log("Oh Hoy!");
            if (modelController != null) {
                modelController.PlayerScoreTracker.AddPoints(points);
                if (_targetScore != null) _targetScore.OnScore(modelController);
            }

        } else {
            AKRender.material = idleTexture;
            Debug.Log("Hee, hee, hee");
        }
    }
}
