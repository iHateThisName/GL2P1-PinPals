using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour {
    [SerializeField] private GameObject _pressed, _unPressed;
    [SerializeField] private UnityEvent ButtonAnimationDone;
    public void OnBackPressed() {
        StartCoroutine(ButtonPressAnimation());
    }

    private IEnumerator ButtonPressAnimation() {
        this._unPressed.SetActive(false);
        this._pressed.SetActive(true);
        yield return new WaitForSecondsRealtime(0.25f);
        this._unPressed.SetActive(true);
        this._pressed.SetActive(false);
        yield return new WaitForSecondsRealtime(0.25f);

        this.ButtonAnimationDone?.Invoke();
    }
}
