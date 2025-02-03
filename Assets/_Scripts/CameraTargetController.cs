using UnityEngine;

// Ivar
public class CameraTargetController : MonoBehaviour {
    [SerializeField] private Transform _playerPinBall;

    [Header("Camera Configuration")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distanceFromGround = 50f;
    [SerializeField] private float _zPosOffset = -10f;
    [SerializeField] private float _zPosOffsetFalling = -25f;
    [SerializeField] private float _cameraBoundingXOffset = 43.5f;
    [SerializeField] private float _cameraBoundingZMinOffset = 15.5f;
    [Tooltip("Speed the camera's x targetPosition moves towards the target targetPosition. Higher value = faster.")]
    [SerializeField] private float _xLerpSpeed = 1f;
    [Tooltip("Speed the camera's z targetPosition moves towards the target targetPosition. Higher value = faster.")]
    [SerializeField] private float _zLerpSpeed = 3f;

    private Transform _ground;
    private Vector3 _groundMin; // Bootom left
    private Vector3 _groundMax; // Top right

    private bool _isFalling = false;
    private float _previousZPosition;
    private float _currentZPosOffset;


    private void Start() {
        SetUp();
        CalculateGroundBounds();
        PositionCamera();
    }

    private void SetUp() {
        this._ground = GameObject.FindGameObjectWithTag("Ground")?.transform;
        this._currentZPosOffset = this._zPosOffset;
        this._previousZPosition = this.gameObject.transform.position.z;
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
        float deltaTime = Time.deltaTime;
        Vector3 targetPosition = this.gameObject.transform.position;
        Vector3 cameraPosition = this._camera.transform.position;

        // Calculate what Offset to use, lerping between the two values
        if (_isFalling) {
            this._currentZPosOffset = Mathf.Lerp(this._currentZPosOffset, this._zPosOffsetFalling, deltaTime * this._zLerpSpeed);
        } else {
            this._currentZPosOffset = Mathf.Lerp(this._currentZPosOffset, this._zPosOffset, deltaTime * (this._zLerpSpeed * 1.2f));
        }

        // Get the raw targetPosition of the camera for following the player
        float x = Mathf.Lerp(cameraPosition.x, targetPosition.x, deltaTime * this._xLerpSpeed);
        float z = Mathf.Lerp(cameraPosition.z, targetPosition.z + this._currentZPosOffset, deltaTime * this._zLerpSpeed);
        Vector3 rawDistance = new Vector3(x, this._distanceFromGround, z);

        // use clamp to ensure the camera is within the ground area
        this._camera.transform.position = new Vector3(
            Mathf.Clamp(rawDistance.x, _groundMin.x + _cameraBoundingXOffset, _groundMax.x - _cameraBoundingXOffset),
            rawDistance.y,
            Mathf.Clamp(rawDistance.z, _groundMin.z + _cameraBoundingZMinOffset, _groundMax.z - _cameraBoundingXOffset)
        );

    }

    private void LateUpdate() {
        if (this._playerPinBall == null) return;

        //this.transform.targetPosition = this._playerPinBall.targetPosition + this.gameObject.transform.parent.targetPosition;
        this.transform.position = this._playerPinBall.position;
        this.transform.rotation = Quaternion.identity; // Dont rotate

        // Detect if the gameObject is falling in the z direction with a minimum speed
        _isFalling = this.gameObject.transform.position.z < _previousZPosition - 0.2f;
        _previousZPosition = this.gameObject.transform.position.z;

        PositionCamera();
    }
}
