using System.Collections;
using UnityEngine;
// Hilmir
public class PlayerTipUI : MonoBehaviour {
    [SerializeField] private GameObject _uiTipPrefab;
    [SerializeField] private PlayerReferences _playerReferences;

    [SerializeField] private float _timeToFadeIn = 1.5f;
    public bool IsFirstTime = true;
    private Coroutine _toolTip;


    private void Start() {
        _uiTipPrefab.GetComponent<CanvasGroup>().alpha = 0.0f;
        _uiTipPrefab.SetActive(true);
    }


    public void PlungerCollider() {
        if (IsFirstTime) {
            _toolTip = StartCoroutine(PlungerToolTipTimer());
        }
        Debug.Log("Player is colliding with plunger");
    }
    public void PlungerColliderExit() {
        DisableToolTip();
        if (_toolTip != null) StopCoroutine(_toolTip);
    }

    private void DisableToolTip() {
        IsFirstTime = false;
        _uiTipPrefab.GetComponent<CanvasGroup>().alpha = 0.0f;
        _uiTipPrefab.SetActive(false);
    }
    private IEnumerator PlungerToolTipTimer() {
        yield return new WaitForSecondsRealtime(5.0f);
        _uiTipPrefab.SetActive(true);
        _uiTipPrefab.GetComponent<CanvasGroup>().alpha = 1.0f;
        yield return new WaitForSecondsRealtime(_timeToFadeIn);
        DisableToolTip();
    }

    //private IEnumerator FadeInUICoroutine() {
    //    yield return new WaitForSeconds(5f);
    //    if (_fadeIn == true)
    //    {
    //        _uiTipPrefab.alpha += _timeToFadeIn;
    //    }
    //}
}
