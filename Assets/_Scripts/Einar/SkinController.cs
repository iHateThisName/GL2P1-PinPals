using UnityEngine;
using UnityEngine.SceneManagement;
public class SkinController : MonoBehaviour {
    [SerializeField] private GameObject skinSelectCanvas;
    [SerializeField] private RandomMaterial _randomMaterial;
    public bool _ready;

    public void NextOption() {
        _randomMaterial.SelectNext();
    }

    public void PreviosOption() {
        _randomMaterial.SelectPrevious();
    }

    public void StartButton() {
        _ready = true;
        skinSelectCanvas.SetActive(false);
        //SceneManager.LoadScene("Prototype");
        CheckPlayerReady();
    }

    private void CheckPlayerReady() {
        int playersReady = 0;

        //GameManager.Instance.Players.Keys = List of EnumPlayerTag
        foreach (EnumPlayerTag tag in GameManager.Instance.Players.Keys) {
            ModelController modelController = GameManager.Instance.GetModelController(tag);
            if (modelController.SkinManager._ready) {
                playersReady++;
            } else {
                return;
            }
        }

        if (playersReady == GameManager.Instance.Players.Count) {
            //All players ready
            SceneManager.LoadScene(PlayerSettings.SelectedLevel);
            //GameManager.Instance.GameModeSelect();
        }
    }
}
