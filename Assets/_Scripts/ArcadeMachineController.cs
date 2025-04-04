using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ArcadeMachineController : MonoBehaviour {
    [SerializeField] private NavigationManager _navigationManager;
    [SerializeField] private bool IsDefault = false;
    [SerializeField] private UnityEvent OnHover;
    [SerializeField] private UnityEvent OnHoverExit;
    [SerializeField] private UnityEvent OnSelect;
    [SerializeField] private UnityEvent OnDeselect;
    [SerializeField] private UnityEvent OnAnimationDone;

    [SerializeField] private GameObject _spotLight;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _cinemachineCamera;
    [SerializeField] private MeshRenderer _screenMeshRenderer;
    [SerializeField] private Material _screenOnMaterial, _screenOffMaterial;

    private bool _isSelected = false;
    private InputAction _cancel;
    private CinemachineBrain _cinemachineBrain;

    [SerializeField] private bool _isLogEnabled = false;
    [SerializeField] private bool _isMouseHoverEnabled = false;

    private void Start() {
        this._cancel = InputSystem.actions.FindAction("Cancel");
        this._cancel.performed += ctx => OnCancel();

        _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        if (IsDefault) {
            HoveringArcadeMachine();
            SelectingArcadeMachine();
        }
    }

    public void NavigationEvent() {
        HoveringArcadeMachine();
    }

    public void SelectedEvent() {
        SelectingArcadeMachine();
        if (this._navigationManager != null) this._navigationManager.LockedNavigation = true; // Lock Navigation
    }
    public void DeselectedEvent() {
        UnHoveringArcadeMachine();
    }

    public void OnMouseEnter() {
        if (!this._isMouseHoverEnabled) return;
        if (this._isLogEnabled) Debug.Log("OnMouseEnter, Hovered over: " + gameObject.name);
        HoveringArcadeMachine();
    }

    private void HoveringArcadeMachine() {
        if (_isSelected) return;

        OnHover?.Invoke();

        _spotLight.SetActive(true);
        _canvas.SetActive(true);
    }

    public void OnMouseExit() {
        if (!this._isMouseHoverEnabled) return;
        if (this._isLogEnabled) Debug.Log("OnMouseExit, No longer hovering over: " + gameObject.name);
        UnHoveringArcadeMachine();
    }

    private void UnHoveringArcadeMachine() {
        if (_isSelected) return;

        OnHoverExit?.Invoke();

        _spotLight.SetActive(false);
        _canvas.SetActive(false);
    }

    private void OnMouseDown() {
        if (!this._isMouseHoverEnabled) return;
        if (this._isLogEnabled) Debug.Log("OnMouseDown, Selected: " + gameObject.name);
        SelectingArcadeMachine();
    }

    private void SelectingArcadeMachine() {
        if (_isSelected) return;

        OnSelect?.Invoke();
        _isSelected = true;

        _cinemachineCamera.SetActive(true);
        _screenMeshRenderer.material = _screenOnMaterial;

        StartCoroutine(WaitForBlendToFinish());
    }

    private void OnCancel() {
        if (this._isLogEnabled) Debug.Log("OnCancel, Deselected: " + gameObject.name);
        if (!_isSelected) {
            if (this._navigationManager != null) {
                this._navigationManager.LockedNavigation = false; // Unlock Navigation
                this._navigationManager.TriggerNavigationEvent();
            }
            return;
        }
        OnDeselect?.Invoke();
        _isSelected = false;

        _spotLight.SetActive(false);
        _cinemachineCamera.SetActive(false);
        _screenMeshRenderer.material = _screenOffMaterial;


    }

    private IEnumerator WaitForBlendToFinish() {
        yield return new WaitForSeconds(0.2f);
        while (_cinemachineBrain.IsBlending) {
            yield return null; // Wait until the next frame
        }
        InvokeAnimationDone();
        //GameManager.Instance.GameModeSelect();
    }

    private void InvokeAnimationDone() {
        OnAnimationDone?.Invoke();
    }
}
