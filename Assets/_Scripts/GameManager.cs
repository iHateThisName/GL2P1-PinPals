using UnityEngine;
// Ivar
public class GameManager : Singleton<GameManager> {

    public bool IsPaused { get; private set; }

    public void PauseGame() {
        IsPaused = true;
        Time.timeScale = 0;
    }
    public void ResumeGame() {
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void ReloadGame() {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    #region Testing
    private void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            LoadScene();
        }
    }
    public void LoadScene() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LocalMultiWithCinemachine") {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LocalMulti");
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LocalMultiWithCinemachine");
        }
    }
    #endregion
}
