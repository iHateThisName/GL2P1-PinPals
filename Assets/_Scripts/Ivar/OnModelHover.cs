using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnModelHover : MonoBehaviour {
    [SerializeField] private bool IsDefault = false;
    [SerializeField] private UnityEvent OnHover;
    [SerializeField] private UnityEvent OnHoverExit;
    [SerializeField] private UnityEvent OnSelect;
    [SerializeField] private UnityEvent OnDeselect;

    private bool _isSelected = false;
    private InputAction _cancel;

    private void Start() {
        this._cancel = InputSystem.actions.FindAction("Cancel");
        this._cancel.performed += ctx => OnCancel();

        if (IsDefault) {
            OnMouseEnter();
            OnMouseDown();
        }
    }
    private void OnMouseEnter() {
        Debug.Log("OnMouseEnter, Hovered over: " + gameObject.name);
        if (_isSelected) return;

        OnHover?.Invoke();
    }

    private void OnMouseExit() {
        Debug.Log("OnMouseExit, No longer hovering over: " + gameObject.name);
        if (_isSelected) return;

        OnHoverExit?.Invoke();
    }
    private void OnMouseDown() {
        Debug.Log("OnMouseDown, Selected: " + gameObject.name);
        if (_isSelected) return;

        OnSelect?.Invoke();
        _isSelected = true;
    }

    private void OnCancel() {
        Debug.Log("OnCancel, Deselected: " + gameObject.name);
        if (!_isSelected) return;
        OnDeselect?.Invoke();
        _isSelected = false;
    }
}
