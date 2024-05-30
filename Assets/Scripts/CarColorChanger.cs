using System.Collections.Generic;
using UnityEngine;

public class CarColorChanger : MonoBehaviour
{
    [SerializeField] private List<Mesh> meshes;
    private MeshFilter meshFilter;
    private int iterator = 0;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            iterator = (iterator + 1) % meshes.Count;
            meshFilter.mesh = meshes[iterator];
        }
    }
}
