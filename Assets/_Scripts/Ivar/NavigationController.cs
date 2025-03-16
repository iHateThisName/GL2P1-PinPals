using UnityEngine;
using UnityEngine.Events;

// Ivar
public class NavigationController : MonoBehaviour {

    [Header("Grid Position")]
    public Vector2Int gridPosition;
    [SerializeField] private bool _isLogEnabled = false;

    [Header("Action Events")]
    public UnityEvent NavigatedEvent;
    public UnityEvent SelectedEvent;
    public UnityEvent DeselectedEvent;

    [Header("Outline Settings")]
    [SerializeField] private GameObject _modelOutline;
    private GameObject _outlineGameObject;
    private Color _outlineColor = Color.black;
    [SerializeField, Range(0f, 1f)] private float _colorAlpha = 0.2f;
    [SerializeField] private float _outlineSize = 0.1f;
    [SerializeField] private Material _outlineBaseMaterial;

    private void Start() {
        SetUp();

        // Log Listeners
        if (this._isLogEnabled) {
            this.NavigatedEvent.AddListener(() => Debug.Log("Navigated: " + this.gameObject.name));
            this.SelectedEvent.AddListener(() => Debug.Log("Selected: " + this.gameObject.name));
            this.DeselectedEvent.AddListener(() => Debug.Log("Deselected: " + this.gameObject.name));
        }

        // Outline Listeners
        this.NavigatedEvent.AddListener(() => EnableOutline());
        this.DeselectedEvent.AddListener(() => DisableOutline());
    }

    private void SetUp() {
        NavigationManager.Instance.RegisterObject(gridPosition, this); // Register in the NavigationManager
        if (this._modelOutline == null) this._modelOutline = this.gameObject; // If no outline is set, use the object itself
    }

    public void EnableOutline() {
        if (this._outlineGameObject == null || !this._outlineGameObject) {
            this._outlineGameObject = Instantiate(_modelOutline, transform.position, transform.rotation, transform);
            this._outlineGameObject.name = "Outline" + this._modelOutline.name;
            Vector3 worldScale = _modelOutline.transform.lossyScale * (1 + _outlineSize);

            // Convert world scale back into local scale relative to the new parent
            _outlineGameObject.transform.localScale = new Vector3(
                worldScale.x / transform.lossyScale.x,
                worldScale.y / transform.lossyScale.y,
                worldScale.z / transform.lossyScale.z
            );

            Renderer outlineRenderer = _outlineGameObject.GetComponent<Renderer>();
            outlineRenderer.material = new Material(this._outlineBaseMaterial);
            this._outlineColor.a = this._colorAlpha;
            outlineRenderer.material.color = _outlineColor;

        } else {
            this._outlineGameObject.SetActive(true);
        }
    }

    public void DisableOutline() {
        if (this._outlineGameObject != null) {
            this._outlineGameObject.SetActive(false);
        }
    }

}
