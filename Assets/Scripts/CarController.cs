using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float motorTorque = 500f;
    [SerializeField] private float brakeTorque = 500f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float maxTurnAngle = 45f;
    [SerializeField] private float maxTurnAngleOnHighSpeed = 15f;
    private Rigidbody _rb;
    private WheelControl[] wheels;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelControl>();
    }

    private void FixedUpdate()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        bool isSpaceDown = Input.GetKey(KeyCode.Space);

        float forwardSpeed = Vector3.Dot(transform.forward, _rb.velocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        float currentMaxTurnAngle = Mathf.Lerp(maxTurnAngle, maxTurnAngleOnHighSpeed, speedFactor);
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        if (Input.GetKey(KeyCode.R))
            CarReset();

        foreach (WheelControl wheel in wheels)
        {
            if (isSpaceDown && wheel.hasParkingBrake)
            {
                wheel.wheelCollider.brakeTorque = brakeTorque;
                continue;
            }
            if (!isSpaceDown && wheel.hasParkingBrake)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
            if (wheel.steerable) 
                wheel.wheelCollider.steerAngle = currentMaxTurnAngle * hInput;
            if (isAccelerating)
            {
                if (wheel.motorized)
                    wheel.wheelCollider.motorTorque = currentMotorTorque * vInput;
                if (!isSpaceDown || !wheel.hasParkingBrake)
                    wheel.wheelCollider.brakeTorque = 0;
            }
            else
            {
                wheel.wheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.wheelCollider.motorTorque = 0;
            }
        }
    }

    private void CarReset()
    {
        Transform carTransform = transform;
        carTransform.position = new Vector3(0, 1, 0);
        carTransform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
