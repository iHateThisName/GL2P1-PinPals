using UnityEngine;

// Ivar
public class CameraTargetController : MonoBehaviour {
    private Transform _playerPinBall;

    private void Start() {
        this._playerPinBall = this.transform.parent.transform;
    }
    private void LateUpdate() {
        if (this._playerPinBall != null) {
            this.transform.position = this._playerPinBall.position; // Copy the position
            this.transform.rotation = Quaternion.identity; // Dont rotate
        }
    }
}
