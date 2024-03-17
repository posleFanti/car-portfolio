using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float springDamper = 10.0f;
    [SerializeField] private float springStregth = 15.0f;
    [SerializeField] private float restLength;
    [SerializeField] private float wheelRadius = 0.3f;
    private Rigidbody _rb;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private Transform[] wheels;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            UpdateWheelPhysics(rayPoints[i], wheels[i]);
        }
    }

    private void UpdateWheelPhysics(Transform rayPoint, Transform wheelTransform)
    {
        RaycastHit hit;
        bool rayDidHit = Physics.Raycast(rayPoint.position, -rayPoint.up, out hit, restLength);

        if (rayDidHit)
        {
            //Debug.Log("hit.point.y: " + hit.point.y);
            //wheelTransform.position = Vector3.MoveTowards(wheelTransform.position, new Vector3(hit.point.x, hit.point.y + wheelRadius, hit.point.z), 1000);
            wheelTransform.position = new Vector3(hit.point.x, hit.point.y + wheelRadius, hit.point.z);
            Vector3 springDir = rayPoint.up;
            Vector3 position = rayPoint.position;
            Vector3 tireWorldVel = _rb.GetPointVelocity(position);
            float offset = restLength - hit.distance;
            float vel = Vector3.Dot(springDir, tireWorldVel);
            float force = (offset * springStregth) - (vel * springDamper);
            _rb.AddForceAtPosition(springDir * force, position);
            Debug.DrawLine(position, hit.point, Color.red);
        }
        else
        {
            //Debug.Log("hit.point.y: " + hit.point.y);
            Debug.DrawLine(rayPoint.position, rayPoint.position + restLength * -rayPoint.up, Color.green);
        }
    }
}