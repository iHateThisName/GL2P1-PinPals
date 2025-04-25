using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// Hilmir
public class PlayerTipUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _uiTipPrefab;
    [SerializeField] private PlayerReferences _playerReferences;

    private void Start() {
        _uiTipPrefab.alpha = 0f;
    }
    /*private void Update() {
        Invoke("FadeInUI", 5f);
    }*/

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Plunger"))
        {
            print("player is colliding with plunger");
            Invoke("FadeInUI", 5f);
        }
        else if (!_playerReferences.gameObject.CompareTag("Plunger"))
        {
            print("player is NOT colliding with plunger");
            _uiTipPrefab.alpha = 0f;
        }
    }
    /*private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Plunger"))
        {
            _uiTipPrefab.alpha = 0f;
        }
    }*/

    private void FadeInUI() {
        _uiTipPrefab.alpha += Time.deltaTime;
    }
}
