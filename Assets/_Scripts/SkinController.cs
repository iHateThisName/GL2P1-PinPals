using UnityEngine;
using UnityEngine.SceneManagement;
public class SkinController : MonoBehaviour {
    [SerializeField] private GameObject skinSelectCanvas;
    [SerializeField] private RandomMaterial _randomMaterial;
    public bool Ready;

    public void NextOption() {
        _randomMaterial.SelectNext();
    }

    public void PreviosOption() {
        _randomMaterial.SelectPrevious();
    }

    public void StartButton() {
        Ready = true;
        skinSelectCanvas.SetActive(false);
        CheckPlayerReady();
    }

    private void CheckPlayerReady() {
        int playersReady = 0;

        foreach (EnumPlayerTag tag in GameManager.Instance.Players.Keys) {
            PlayerReferences modelController = GameManager.Instance.GetPlayerReferences(tag);
            if (modelController.SkinController.Ready) {
                playersReady++;
            } else {
                return;
            }
        }

        if (playersReady == GameManager.Instance.Players.Count) {
            //All players ready
            SceneManager.LoadScene(PlayerSettings.SelectedLevel);
        }
    }

    public Material GetMaterial() => _randomMaterial.CurrentMaterial();
    public Color GetColor() => _randomMaterial.AssignedMaterialColor;
}
