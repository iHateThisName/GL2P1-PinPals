using UnityEngine;

// Ivar
public class CameraTargetController : MonoBehaviour {
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distanceFromGround = 30f;

    [SerializeField] private Transform _playerPinBall;
    private Transform _ground;

    private void Start() {
        SetUp();
        PositionCamera();
    }

    private void SetUp() {
        this._ground = GameObject.FindGameObjectWithTag("Ground").transform;
        if (_ground == null) Debug.LogError("Ground not found in the scene. Please add a GameObject with the tag 'Ground'");
    }

    private void PositionCamera() {
        //// Move the camera back along the ground's forward vector
        //_camera.transform.position = _ground.position - _ground.forward * _distanceFromGround;
        //// Ensure the camera looks at the ground
        //_camera.transform.LookAt(_ground.position);

        if (this._camera == null) return;
        // Calculate the camera position
       // Vector3 cameraFollowPosition = _ground.position;
        Vector3 cameraFollowPosition = this.gameObject.transform.position;
        //cameraFollowPosition.z = this.gameObject.transform.position.z; // Follow player's height, this will make the camera feel like its zooming out when the player is moving up

        Vector3 cameraOffset = _ground.up * _distanceFromGround + -_ground.forward * _distanceFromGround;
        _camera.transform.position = cameraFollowPosition + cameraOffset;
    }

    private void LateUpdate() {
        if (this._playerPinBall == null) return;
        this.transform.position = this._playerPinBall.position + this.gameObject.transform.parent.position; // Copy the position
        this.transform.rotation = Quaternion.identity; // Dont rotate

        PositionCamera();
    }
}
