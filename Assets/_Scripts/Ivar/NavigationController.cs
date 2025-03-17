using UnityEngine;
using UnityEngine.Events;

// Ivar
public class NavigationController : MonoBehaviour {

    [Header("Grid Position")]
    [SerializeField] private NavigationManager _navigationManager;
    public Vector2Int gridPosition;
    [SerializeField] private bool _isLogEnabled = false;

    [Header("Action Events")]
    public UnityEvent NavigatedEvent;
    public UnityEvent SelectedEvent;
    public UnityEvent DeselectedEvent;

    [Header("Outline Settings")]
    [SerializeField] private bool _UseOutline = true;
    [SerializeField] private GameObject _modelOutline;
    private GameObject _outlineGameObject;
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
        if (this._UseOutline) {
            this.NavigatedEvent.AddListener(() => EnableOutline());
            this.DeselectedEvent.AddListener(() => DisableOutline());
        }

        UpdateOutlineMaterialColor();

    }

    private void SetUp() {
        _navigationManager.RegisterObject(gridPosition, this); // Register in the NavigationManager
        if (this._modelOutline == null) this._modelOutline = this.gameObject; // If no outline is set, use the object itself
    }

    public void EnableOutline() {
        if (this._outlineGameObject == null || !this._outlineGameObject) {
            if (CheckForOutlineGameobject(true)) return;
            this._outlineGameObject = Instantiate(_modelOutline, transform.position, transform.rotation, transform);
            this._outlineGameObject.name = "Outline" + this._modelOutline.name;
            Vector3 worldScale = _modelOutline.transform.lossyScale * (1 + _outlineSize);

            // Convert world scale back into local scale relative to the new parent
            _outlineGameObject.transform.localScale = new Vector3(
                worldScale.x / transform.lossyScale.x,
                worldScale.y / transform.lossyScale.y,
                worldScale.z / transform.lossyScale.z
            );

            UpdateOutlineMaterialColor();
            this._outlineGameObject.SetActive(true);

        } else {
            UpdateOutlineMaterialColor();
            this._outlineGameObject.SetActive(true);
        }
    }

    public void UpdateOutlineMaterialColor() {
        if (this._outlineGameObject == null) return;
        Renderer outlineRenderer = this._outlineGameObject.GetComponent<Renderer>();
        Color color = this._navigationManager.GetPlayerColor();
        color.a = this._colorAlpha;
        foreach (var material in outlineRenderer.materials) {
            var newMaterial = new Material(this._outlineBaseMaterial);
            newMaterial.color = color;
            material.CopyPropertiesFromMaterial(newMaterial);
        }
    }

    private bool CheckForOutlineGameobject(bool active) {
        GameObject outlineGameobject = _modelOutline.transform.Find("Outline" + _modelOutline.name)?.gameObject;
        if (outlineGameobject != null) {
            this._outlineGameObject = outlineGameobject;
            this._outlineGameObject.SetActive(active);
            return true;
        }
        return false;
    }

    public void DisableOutline() {
        if (this._outlineGameObject != null) {
            this._outlineGameObject.SetActive(false);
        } else {
            CheckForOutlineGameobject(false);
        }
    }

}
