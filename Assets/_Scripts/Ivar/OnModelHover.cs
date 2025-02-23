using UnityEngine;
using UnityEngine.Events;

public class OnModelHover : MonoBehaviour {

    [SerializeField] private UnityEvent OnHover;
    [SerializeField] private UnityEvent OnHoverExit;
    [SerializeField] private UnityEvent OnSelect;
    private void OnMouseEnter() {
        Debug.Log("OnMouseEnter, Hovered over: " + gameObject.name);
        OnHover?.Invoke();
    }

    private void OnMouseExit() {
        Debug.Log("OnMouseExit, No longer hovering over: " + gameObject.name);
        OnHoverExit?.Invoke();
    }
    private void OnMouseDown() {
        Debug.Log("OnMouseDown, Selected: " + gameObject.name);
        OnSelect?.Invoke();
    }
}
