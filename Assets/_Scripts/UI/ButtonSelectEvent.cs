using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelectEvent : MonoBehaviour, ISelectHandler {
    [SerializeField] private UnityEvent OnSelectUnityEvent;
    public void OnSelect(BaseEventData eventData) {
        OnSelectUnityEvent?.Invoke();
    }
}
