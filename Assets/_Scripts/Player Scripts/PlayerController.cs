using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Ivar
public class PlayerController : MonoBehaviour {
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private Rigidbody _ballRigidbody;
    private List<FlipperController> _leftFlipperController;
    private List<FlipperController> _rightFlipperController;

    // Input Actions Varibals
    private Vector2 _movementInput = Vector2.zero;

    private void Awake() {
        SceneManager.sceneLoaded -= OnSceneLoaded; // to prevent duplicate subscriptions when Awake() is called multiple times.
        SceneManager.sceneLoaded += OnSceneLoaded; // gets called every time a scene is loaded
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        AssignFlippers();
    }

    private void OnEnable() {
        if (_ballRigidbody == null) Debug.LogWarning("Ball Rigidbody is not assigned! Please assign it in the inspector.");
        AssignFlippers();
    }

    private void AssignFlippers() {
        GameObject[] leftFlippers = GameObject.FindGameObjectsWithTag(tag: "LeftFlipper");
        GameObject[] rightFlippers = GameObject.FindGameObjectsWithTag(tag: "RightFlipper");
        _leftFlipperController = new List<FlipperController>();
        _rightFlipperController = new List<FlipperController>();

        string playerLayerString = LayerMask.LayerToName(this.gameObject.layer);
        string playerSuffix = playerLayerString.Substring(playerLayerString.Length - 2);

        if (leftFlippers.Length == 0 || rightFlippers.Length == 0) {
            Debug.LogWarning("No flippers found! Please add flippers to the scene and tag them");
            return;
        }

        ProcessFlippers(leftFlippers, playerSuffix);
        ProcessFlippers(rightFlippers, playerSuffix);
    }

    public void ProcessFlippers(GameObject[] flippers, string playerSuffix) {
        foreach (GameObject flipper in flippers) {
            string flipperLayerString = LayerMask.LayerToName(flipper.layer);
            string flipperSuffix = flipperLayerString.Substring(flipperLayerString.Length - 2);

            if (flipperSuffix == playerSuffix) {
                _leftFlipperController.Add(flipper.GetComponent<FlipperController>());
            }
        }
    }

    // Input Actions Methods
    public void OnMove(InputAction.CallbackContext context) => this._movementInput = context.ReadValue<Vector2>();
    public void OnFlipperLeft(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            AllFlipLeft(true);
        } else if (context.phase == InputActionPhase.Canceled) {
            AllFlipLeft(false);
        }
    }
    public void OnFlipperRight(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            AllFlipRight(true);
        } else if (context.phase == InputActionPhase.Canceled) {
            AllFlipRight(false);
        }
    }

    private void AllFlipLeft(bool isFlipping) {
        foreach (FlipperController flipper in _leftFlipperController) {
            flipper.IsFlipping = isFlipping;
        }
    }

    private void AllFlipRight(bool isFlipping) {
        foreach (FlipperController flipper in _rightFlipperController) {
            flipper.IsFlipping = isFlipping;
        }
    }

    void FixedUpdate() {
        _ballRigidbody.AddTorque(new Vector3(this._movementInput.y, 0, -this._movementInput.x) * _playerSpeed, ForceMode.Force);
    }

    public void DisableGravity() {
        _ballRigidbody.isKinematic = true;
    }
    public void EnableGravity() {
        _ballRigidbody.isKinematic = false;
    }
}
