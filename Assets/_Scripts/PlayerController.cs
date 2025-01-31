using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private Rigidbody _ballRigidbody;
    private List<FlipperController> _leftFlipperController = new List<FlipperController>();
    private List<FlipperController> _rightFlipperController = new List<FlipperController>();

    // Input Actions Varibals
    private Vector2 _movementInput = Vector2.zero;

    private void Start() {
        if (_ballRigidbody == null) Debug.LogError("Ball Rigidbody is not assigned! Please assign it in the inspector.");

        GameObject[] leftFlippers = GameObject.FindGameObjectsWithTag(tag: "LeftFlipper");
        GameObject[] rightFlippers = GameObject.FindGameObjectsWithTag(tag: "RightFlipper");

        string playerLayerString = LayerMask.LayerToName(this.gameObject.layer);
        string playerSuffix = playerLayerString.Substring(playerLayerString.Length - 2);
        foreach (GameObject flipper in leftFlippers) {
            string flipperLayerString = LayerMask.LayerToName(flipper.layer);
            string flipperSuffix = LayerMask.LayerToName(flipper.layer).Substring(flipperLayerString.Length - 2);

            if (flipperSuffix == playerSuffix) {
                _leftFlipperController.Add(flipper.GetComponent<FlipperController>());
            }
        }
        foreach (GameObject flipper in rightFlippers) {
            string flipperLayerString = LayerMask.LayerToName(flipper.layer);
            string flipperSuffix = LayerMask.LayerToName(flipper.layer).Substring(flipperLayerString.Length - 2);
            if (flipperSuffix == playerSuffix) {
                _rightFlipperController.Add(flipper.GetComponent<FlipperController>());
            }
        }

        if (leftFlippers.Length == 0 || rightFlippers.Length == 0) Debug.LogError("No flippers found! Please add flippers to the scene and tag them");
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
        //Vector3 move = new Vector3(this._movementInput.x, 0, this._movementInput.y);
        //_ballRigidbody.MovePosition(_ballRigidbody.position + move * _playerSpeed * Time.fixedDeltaTime);
        //_ballRigidbody.AddTorque(move * _playerSpeed * Time.fixedDeltaTime);
        _ballRigidbody.AddTorque(new Vector3(this._movementInput.y, 0, -this._movementInput.x) * _playerSpeed, ForceMode.Force);
    }

    public void DisableGravity() {
        _ballRigidbody.isKinematic = true;
    }
    public void EnableGravity() {
        _ballRigidbody.isKinematic = false;
    }
}
