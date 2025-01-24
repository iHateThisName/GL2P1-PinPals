using UnityEngine;

public class FlipperController : MonoBehaviour {
    public KeyCode activationKey; // Key to activate the flipper
    public float restPosition = 0f; // Default angle
    public float activePosition = 45f; // Angle when activated
    public float flipSpeed = 10f; // Speed of flipping

    private HingeJoint hinge;

    public bool IsFlipping = false;

    void Start() {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = hinge.spring;
        spring.spring = 10000f; // Strength of the flipper
        hinge.spring = spring;
        hinge.useSpring = true;
    }

    private void FixedUpdate() {
        JointSpring spring = hinge.spring;
        if (Input.GetKey(activationKey) || IsFlipping) {
            spring.targetPosition = activePosition;
        } else {
            spring.targetPosition = restPosition;
        }
        hinge.spring = spring;
    }
}
