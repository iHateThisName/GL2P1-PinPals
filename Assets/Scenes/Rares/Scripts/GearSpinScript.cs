using UnityEngine;

public class GearSpinScript : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    public enum SpinDirection { Right, Left }
    public SpinDirection spinDirection = SpinDirection.Right;

    void Update()
    {
        float directionMultiplier = (spinDirection == SpinDirection.Right) ? 1f : -1f;
        transform.Rotate(rotationSpeed * directionMultiplier * Time.deltaTime);
    }
}
