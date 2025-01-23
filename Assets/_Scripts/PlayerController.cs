using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private Rigidbody _ballRigidbody;

    // Input Actions Varibals
    private Vector2 _movementInput = Vector2.zero;

    private void Start() {
        if (_ballRigidbody == null) Debug.LogError("Ball Rigidbody is not assigned! Please assign it in the inspector.");
    }

    // Input Actions Methods
    public void OnMove(InputAction.CallbackContext context) => this._movementInput = context.ReadValue<Vector2>();

    void Update() {
    }

    void FixedUpdate() {
        //Vector3 move = new Vector3(this._movementInput.x, 0, this._movementInput.y);
        //_ballRigidbody.MovePosition(_ballRigidbody.position + move * _playerSpeed * Time.fixedDeltaTime);
        //_ballRigidbody.AddTorque(move * _playerSpeed * Time.fixedDeltaTime);
        _ballRigidbody.AddTorque(new Vector3(this._movementInput.y, 0, -this._movementInput.x) * _playerSpeed, ForceMode.Force);

    }
}
