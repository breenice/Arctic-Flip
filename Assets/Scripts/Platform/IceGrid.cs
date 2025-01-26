using UnityEngine;
public class IceGrid : MonoBehaviour    
{
    private Outliner iceController;
    private FeedbackController textController;
    public Slot[] slots;
    public Slot targetSlot;
    private void Start()
    {
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag("slot");
        slots = new Slot[slotObjects.Length];

        for (int i = 0; i < slotObjects.Length; i++)
        {
            Slot slotComponent = slotObjects[i].GetComponent<Slot>();

            if (slotComponent == null)
            {
                // If the GameObject doesn't have a Slot component, add it dynamically
                slotComponent = slotObjects[i].AddComponent<Slot>();
            }

            slots[i] = slotComponent;
        }
    }

    public void Update()
    {
        getTargetSlot();
    }

    private void getTargetSlot()
    {
        foreach (Slot slot in slots)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = slot.GetComponent<Collider2D>();
            if (collider != null && collider.OverlapPoint(mousePos))
            {
                targetSlot = slot;
                break;
            }
        }
    }
    
    private void onMourseUp()
    {
        textController.SetFeedbackText("Mouse Up!");
        if (targetSlot != null && targetSlot.isAvailable)
        {
            textController.SetFeedbackText("Placing ice...");
            if (iceController.selectedIce != null)
            {
                textController.SetFeedbackText("ice picked...");
                iceController.selectedIce.transform.position = targetSlot.transform.position;
                targetSlot.PlaceIce(iceController.selectedIce);
                textController.SetFeedbackText("ice placed!");
            }
        }
        else
        {
            if (iceController.selectedIce != null)
            { 
                iceController.selectedIce.transform.position = iceController.selectedIce_ogPosition;
            }
        }
    }
}