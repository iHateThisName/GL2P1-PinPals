using System.Collections;
using UnityEngine;
using TMPro;
// Ivar & Hilmir
public class ScoreAnimationController : MonoBehaviour {
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _canvasScoreText;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _gradientSpeed = 0.1f;

    private float _totalTime;
    
    private void Start() {
        if (_camera == null) _camera = Camera.main;
        if (_canvas == null) Debug.LogWarning("Canvas is not assigned to ScoreAnimationController, " + gameObject.name);
        Destroy(gameObject, 3.5f);
        StartCoroutine(AssignRandomColours());
    }
    public void AssaigneScoreText(string text) => _canvasScoreText.text = text;

    public IEnumerator AssignRandomColours() {
        _canvasScoreText.ForceMeshUpdate();

        TMP_TextInfo textInfo = _canvasScoreText.textInfo;
        int currentChar = 0;

        Color32[] newVertexColours;
        Color32 color = _canvasScoreText.color;

        while (true) {
            int charCount = textInfo.characterCount;

            if (charCount == 0) {
                yield return new WaitForSeconds(.25f);
                continue;
            }

            int matIndex = textInfo.characterInfo[currentChar].materialReferenceIndex;
            newVertexColours = textInfo.meshInfo[matIndex].colors32;
            int vertexIndex = textInfo.characterInfo[currentChar].vertexIndex;

            if (textInfo.characterInfo[currentChar].isVisible) {
                var offset = currentChar / charCount;
                color = _gradient.Evaluate((_totalTime + offset) % 1f);
                _totalTime += Time.deltaTime;

                newVertexColours[vertexIndex + 0] = color;
                newVertexColours[vertexIndex + 1] = color;
                newVertexColours[vertexIndex + 2] = color;
                newVertexColours[vertexIndex + 3] = color;

                _canvasScoreText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
            currentChar = (currentChar + 1) % charCount;

            yield return new WaitForSeconds(_gradientSpeed);

        }
    }
}
