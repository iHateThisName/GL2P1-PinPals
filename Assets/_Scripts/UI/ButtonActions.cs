using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour {

    [Header("Setup")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Selectable _elementToSelect;

    [Header("HowToPlay")]
    [SerializeField] private List<GameObject> _controllerInstructions;
    [SerializeField] private List<GameObject> _powerUpInstructions;
    private int _currentControllerInstructionIndex = 0, _currentPowerUpInstructionIndex = 0;
    [SerializeField] private GameObject _backPressed, _backUnPressed;

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

    public void JumpToElement(Selectable element) {
        if (_eventSystem == null)
            Debug.LogWarning("Missing EventSystem reference.", this);
        else if (element == null)
            Debug.LogWarning("JumpToElement received null element.", this);
        else
            StartCoroutine(DelayedSelect(element));
    }

    private IEnumerator DelayedSelect(Selectable element) {
        yield return null; // Wait until the end of the frame
        _eventSystem.SetSelectedGameObject(element.gameObject);
    }

    #region How To Play

    public void EnabledControllerInstruction() => _controllerInstructions[_currentControllerInstructionIndex].SetActive(true);
    public void DisableControllerInstruction() => _controllerInstructions[_currentControllerInstructionIndex].SetActive(false);
    public void EnablePowerUpInstructions() => _powerUpInstructions[_currentPowerUpInstructionIndex].SetActive(true);
    public void DisablePowerUpInstructions() => _powerUpInstructions[_currentPowerUpInstructionIndex].SetActive(false);

    public void OnControllerInstruction(bool isNextPressed) {
        if (isNextPressed) {
            // Disable the current instruction
            _controllerInstructions[_currentControllerInstructionIndex].SetActive(false);

            // Select the new Index
            _currentControllerInstructionIndex++;
            if (_currentControllerInstructionIndex >= _controllerInstructions.Count) {
                _currentControllerInstructionIndex = 0;
            }
            // Enable the next instruction
            _controllerInstructions[_currentControllerInstructionIndex].SetActive(true);
        } else {
            // Disable the current instruction
            _controllerInstructions[_currentControllerInstructionIndex].SetActive(false);

            // Select the new Index
            _currentControllerInstructionIndex--;
            if (_currentControllerInstructionIndex < 0) {
                _currentControllerInstructionIndex = _controllerInstructions.Count - 1;
            }

            // Enable the next instruction
            _controllerInstructions[_currentControllerInstructionIndex].SetActive(true);
        }
    }
    public void OnPowerUpInstruction(bool isNextPressed) {
        if (isNextPressed) {
            // Disable the current instruction
            _powerUpInstructions[_currentPowerUpInstructionIndex].SetActive(false);

            // Select the new Index
            _currentPowerUpInstructionIndex++;
            if (_currentPowerUpInstructionIndex >= _powerUpInstructions.Count) {
                _currentPowerUpInstructionIndex = 0;
            }
            // Enable the next instruction
            _powerUpInstructions[_currentPowerUpInstructionIndex].SetActive(true);
        } else {
            // Disable the current instruction
            _powerUpInstructions[_currentPowerUpInstructionIndex].SetActive(false);

            // Select the new Index
            _currentPowerUpInstructionIndex--;
            if (_currentPowerUpInstructionIndex < 0) {
                _currentPowerUpInstructionIndex = _powerUpInstructions.Count - 1;
            }

            // Enable the next instruction
            _powerUpInstructions[_currentPowerUpInstructionIndex].SetActive(true);
        }
    }

    public void OnBack() {
        this._backUnPressed.SetActive(false);
        this._backPressed.SetActive(true);
        StartCoroutine(ReturnToMainMenu());
    }
    private IEnumerator ReturnToMainMenu() {
        yield return new WaitForSecondsRealtime(0.25f);
        this._backUnPressed.SetActive(true);
        this._backPressed.SetActive(false);
        yield return new WaitForSecondsRealtime(0.25f);
        GameManager.Instance.MainMenu();
    }
    #endregion
}
