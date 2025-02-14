using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MotorScript : MonoBehaviour
{
    private HingeJoint hinge;
    private bool _isSpinning = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Player") && _isSpinning == false)
        {
            JointMotor motor = hinge.motor;
            motor.targetVelocity = 200;
            motor.force = 1000;
            motor.freeSpin = false;
            hinge.motor = motor;
            hinge.useMotor = true;

            _isSpinning = true;
            StartCoroutine(AllowSpin());
        }
    }

    private IEnumerator AllowSpin()
    {
        yield return new WaitForSecondsRealtime(1);
        _isSpinning = false;
    }
}
