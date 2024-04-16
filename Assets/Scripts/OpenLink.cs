using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isActivated", true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
            Application.OpenURL(url);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isActivated", false);
    }
}
