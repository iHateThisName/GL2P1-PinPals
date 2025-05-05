using UnityEngine;

public class FlipperController : MonoBehaviour {
    public float restPosition = 0f; // Default angle
    public float activePosition = 45f; // Angle when activated
    public float flipSpeed = 10f; // Speed of flipping
    [SerializeField] private int _renderQueue = 1;
    private HingeJoint hinge;

    public bool IsFlipping = false;

    private MeshRenderer _meshRenderer;
    private void Awake() {
        this._meshRenderer = GetComponent<MeshRenderer>();
        this._meshRenderer.enabled = false;
    }
    void Start() {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = hinge.spring;
        spring.spring = 10000f; // Strength of the flipper
        hinge.spring = spring;
        hinge.useSpring = true;

        // Set the render queue
        //this._meshRenderer.material.renderQueue = _renderQueue * 3000;
    }

    public void SetFlipperColor(Color color) {
        this._meshRenderer.enabled = true;
        this._meshRenderer.materials[0].color = color;
    }

    private void FixedUpdate() {
        JointSpring spring = hinge.spring;
        if (IsFlipping) {
            spring.targetPosition = activePosition;
        } else {
            spring.targetPosition = restPosition;
        }
        hinge.spring = spring;
    }
}
