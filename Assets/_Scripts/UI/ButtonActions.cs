using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour {

    [Header("Setup")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Selectable _elementToSelect;

    void Start() {
        _eventSystem = GameObject.FindAnyObjectByType<EventSystem>();
        JumpToElement();
    }
    public void OnResumeClick() => GameManager.Instance.OnPauseAction();
    public void OnRestartClick() => GameManager.Instance.ReloadGame();
    public void OnMainMenuClick() => GameManager.Instance.MainMenu();

    public void JumpToElement() {
        if (this._eventSystem == null)
            Debug.Log("Missing system referenced.", this);

        if (this._elementToSelect == null)
            Debug.Log("Missing UI element reference.", this);

        this._eventSystem.SetSelectedGameObject(this._elementToSelect.gameObject);
    }
}
