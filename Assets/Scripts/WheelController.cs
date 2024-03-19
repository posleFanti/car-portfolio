using System;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float acceleration = 500f;
    [SerializeField] private float braking = 300f;
    [SerializeField] private float currentAcceleration = 0f;
    [SerializeField] private float currentBrakeForce = 0f;
    [SerializeField] private float currentTurnAngle = 0f;
    [SerializeField] private float maxTurnAngle = 30f;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
            ResetPosition();
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        currentBrakeForce = Input.GetKey(KeyCode.Space) ? braking : 0f;
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        for (int i = 0; i < 2; i++)
        {
            wheelColliders[3-i].motorTorque = currentAcceleration;
            wheelColliders[i].steerAngle = currentTurnAngle;
        }
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].brakeTorque = currentBrakeForce;
            UpdateWheelMeshes(wheelColliders[i], wheels[i]);
        }
    }

    private void UpdateWheelMeshes(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rotation);
        wheelTransform.position = pos;
        wheelTransform.rotation = rotation;
    }

    private void ResetPosition()
    {
        this.transform.position = new Vector3(0, 1, 0);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        foreach (WheelCollider wheelCollider in wheelColliders)
            wheelCollider.rotationSpeed = 0f;
    }
}
