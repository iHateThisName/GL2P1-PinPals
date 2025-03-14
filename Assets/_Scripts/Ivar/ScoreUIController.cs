using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ivar
// This class is responsible for updating the score UI text and color
public class ScoreUIController : MonoBehaviour {

    [SerializeField] private Image UIImage;
    [SerializeField] private TMP_Text playerText;

    public void SetUIText(string text) {
        playerText.text = text;
    }

    public void SetColor(Color color) {
        UIImage.color = color;
    }
}
