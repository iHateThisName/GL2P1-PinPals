using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// Hilmir
public class PlayerTipUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _uiTipPrefab;
    [SerializeField] private PlayerReferences _playerReferences;

    private bool _fadeIn = false;
    [SerializeField] private float _timeToFadeIn;

    private void Start() {
        _uiTipPrefab.alpha = 0f;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Plunger"))
        {
            _fadeIn = true;
            print("player is colliding with plunger");
            // Invoke("FadeInUI", 5f);
            StartCoroutine(FadeInUICoroutine());
        }
        else
        {
            print("player is NOT colliding with plunger");
            _uiTipPrefab.alpha = 0f;
        }
    }

    private IEnumerator FadeInUICoroutine() {
        yield return new WaitForSeconds(5f);
        if (_fadeIn == true)
        {
            _uiTipPrefab.alpha += _timeToFadeIn * Time.deltaTime;
        }
    }
}
