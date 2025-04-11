using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
// Ivar

public class CameraManager : MonoBehaviour {

    [SerializeField] private List<CinemachineCamera> cameras;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        cameras.Sort((x, y) => y.Priority.Value.CompareTo(x.Priority.Value));
    }
}
