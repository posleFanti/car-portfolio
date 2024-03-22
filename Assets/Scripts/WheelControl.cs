using UnityEngine;

public class WheelControl : MonoBehaviour
{
    [SerializeField] private Transform wheelTransform;
    public bool steerable;
    public bool motorized;
    [HideInInspector] public WheelCollider wheelCollider;
    
    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void FixedUpdate()
    {
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
