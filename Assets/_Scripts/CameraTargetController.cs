using UnityEngine;

// Ivar
public class CameraTargetController : MonoBehaviour {
    [SerializeField] private Transform _playerPinBall;

    [Header("Camera Configuration")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distanceFromGround = 30f;
    [SerializeField] private float _zPosOffset = 10f;
    [SerializeField] private float _zPosOffsetFalling = -10f;
    [SerializeField] private float _cameraBoundingXOffset = 43.5f;
    [SerializeField] private float _cameraBoundingZMinOffset = 15.5f;
    [SerializeField] private float _xLerpSpeed = 5f;
    [SerializeField] private float _zLerpSpeed = 3f;

    private Transform _ground;
    private Vector3 _groundMin; // Bootom left
    private Vector3 _groundMax; // Top right

    private void Start() {
        SetUp();
        CalculateGroundBounds();
        PositionCamera();
    }

    private void SetUp() {
        this._ground = GameObject.FindGameObjectWithTag("Ground")?.transform;
        if (_ground == null) Debug.LogError("Ground not found in the scene. Please add a GameObject with the tag 'Ground'");
    }

    private void CalculateGroundBounds() {
        if (_ground == null) return;
        Renderer groundRenderer = _ground.GetComponent<Renderer>();
        if (groundRenderer != null) {
            _groundMin = groundRenderer.bounds.min;
            _groundMax = groundRenderer.bounds.max;
        }
    }

    private void PositionCamera() {
        if (this._camera == null) return;


        Vector3 rawDistance = new Vector3(this.gameObject.transform.position.x, _distanceFromGround, this.gameObject.transform.position.z + this._zPosOffset);

        // use clamp to ensure the camera is within the ground area
        _camera.transform.position = new Vector3(
            Mathf.Clamp(rawDistance.x, _groundMin.x + _cameraBoundingXOffset, _groundMax.x - _cameraBoundingXOffset),
            rawDistance.y,
            Mathf.Clamp(rawDistance.z, _groundMin.z + _cameraBoundingZMinOffset, _groundMax.z - _cameraBoundingXOffset)
        );

    }

    private void FixedUpdate() {
        if (this._playerPinBall == null) return;

        //this.transform.position = this._playerPinBall.position + this.gameObject.transform.parent.position;
        this.transform.position = this._playerPinBall.position;
        this.transform.rotation = Quaternion.identity; // Dont rotate

        PositionCamera();
    }
}
