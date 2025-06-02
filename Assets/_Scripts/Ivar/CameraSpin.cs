using UnityEngine;

public class CameraSpin : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10f;

    private void Update() {
        if (target != null) {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
