using UnityEngine;

public class DestroyMesh : MonoBehaviour
{
    private void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Destroy(meshFilter);
        Destroy(meshRenderer);
    }
}
