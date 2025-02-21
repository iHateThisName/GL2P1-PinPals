using UnityEngine;

public class ButtonActions : MonoBehaviour {

    public void OnResumeClick() => GameManager.Instance.OnPauseAction();
    public void OnRestartClick() => GameManager.Instance.ReloadGame();
    public void OnMainMenuClick() => GameManager.Instance.MainMenu();
}
