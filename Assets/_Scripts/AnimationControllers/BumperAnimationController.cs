using System;
using UnityEngine;

public class BumperAnimationController : MonoBehaviour {
    [SerializeField] private GameObject _animationRipplePrefab;

    public void PlayAnimation(Collision collision) {
        GameObject animationRipple = Instantiate(_animationRipplePrefab, collision.contacts[0].point, Quaternion.identity);
        Destroy(animationRipple, 1.25f);
    }
}
