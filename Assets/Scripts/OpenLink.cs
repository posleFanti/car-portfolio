using System;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private Animator animator;
    private ColorUIChange colorUIChange;

    private void Start()
    {
        colorUIChange = GetComponent<ColorUIChange>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //animator.Play("Base Layer.Highlighting");
    }

    private void OnTriggerStay(Collider other)
    {
        //colorUIChange.ChangeColor();
        if (Input.GetKey(KeyCode.E))
            Application.OpenURL(url);
    }

    private void OnTriggerExit(Collider other)
    {
        //colorUIChange.ResetAlpha();
    }
}
