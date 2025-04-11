using TMPro;
using UnityEngine;

public class ScoreAnimationController : MonoBehaviour {
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _canvasScoreText;

    private void Start() {
        if (_camera == null) _camera = Camera.main;
        if (_canvas == null) Debug.LogWarning("Canvas is not assigned to ScoreAnimationController, " + gameObject.name);
        Destroy(gameObject, 3.5f);
    }
    public void AssaigneScoreText(string text) => _canvasScoreText.text = text;
}
