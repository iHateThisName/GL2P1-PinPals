using UnityEngine;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour {
    [SerializeField] private bool isToggled = false;
    [SerializeField] private UnityEvent UnityToggleOn, UnityToggleOff;

    public void Toggle() {
        isToggled = !isToggled;

        if (isToggled) {
            UnityToggleOn.Invoke();
        } else {
            UnityToggleOff.Invoke();
        }
    }
}
