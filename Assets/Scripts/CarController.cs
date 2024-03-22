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

        float forwardSpeed = Vector3.Dot(transform.forward, _rb.velocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        float currentMaxTurnAngle = Mathf.Lerp(maxTurnAngle, maxTurnAngleOnHighSpeed, speedFactor);
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (WheelControl wheel in wheels)
        {
            if (wheel.steerable) 
                wheel.wheelCollider.steerAngle = currentMaxTurnAngle * hInput;
            if (isAccelerating)
            {
                if (wheel.motorized)
                    wheel.wheelCollider.motorTorque = currentMotorTorque * vInput;
                wheel.wheelCollider.brakeTorque = 0;
            }
            else
            {
                wheel.wheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.wheelCollider.motorTorque = 0;
            }
        }
    }
}
