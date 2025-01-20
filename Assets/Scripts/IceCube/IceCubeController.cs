using UnityEngine;

public class IceCubeOutline : MonoBehaviour
{
    public GameObject iceCubeObject; // Reference to the ice GameObject
    private Material originalMaterial;
    public Material outlineMaterial;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    private void OnMouseEnter()
    {
        if (objectRenderer != null && outlineMaterial != null)
        {
            objectRenderer.material = outlineMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (objectRenderer != null && originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }
    }
}

