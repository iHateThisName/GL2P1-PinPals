using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelectEvent : MonoBehaviour, ISelectHandler {
    [SerializeField] private UnityEvent OnSelectUnityEvent;
    [SerializeField] private GameObject pressedGameObject, unPressedGameObject;
    public void OnSelect(BaseEventData eventData) {
        if (pressedGameObject != null || unPressedGameObject != null)
            StartCoroutine(DelayPressed());
        else
            OnSelectUnityEvent?.Invoke();
    }

    private IEnumerator DelayPressed() {
        pressedGameObject.SetActive(true);
        unPressedGameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        pressedGameObject.SetActive(false);
        unPressedGameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        OnSelectUnityEvent?.Invoke();
    }
}
