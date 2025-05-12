using UnityEngine;

public class DisableIfWebBuild : MonoBehaviour {
    void Awake() {
#if UNITY_WEBGL && !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }
}
