using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isAvailable { get; private set; } = true;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.green;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void PlaceIce(GameObject ice)
    {
        isAvailable = false;
        ice.transform.position = transform.position; 
    }

    public void RemoveIce(GameObject ice)
    {
        // Remove the ice and make the slot available again
        isAvailable = true;
    }
    private void OnMouseOver()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor; 
        }
    }
        private void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor; 
        }
    }
}
