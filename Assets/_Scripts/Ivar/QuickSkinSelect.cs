using UnityEngine;
using UnityEngine.InputSystem;

public class QuickSkinSelect : MonoBehaviour {

    [SerializeField] private SkinController skinController;

    public void OnNextOption(InputAction.CallbackContext context) {
        if (context.performed) skinController.NextOption();
    }

    public void OnPreviousOption(InputAction.CallbackContext context) {
        if (context.performed) skinController.PreviosOption();
    }
}
