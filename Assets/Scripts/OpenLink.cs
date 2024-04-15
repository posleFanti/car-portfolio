using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField] public string url;

    /*private void OnCollisionStay(Collision other)
    {
        Debug.Log(other.rigidbody);
        if (Input.GetKey(KeyCode.E)) 
            Application.OpenURL(url);
    }*/

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.GetComponent<Rigidbody>());
        if (Input.GetKey(KeyCode.E))
            Application.OpenURL(url);
    }
}
