using UnityEngine;
public class IceGrid : MonoBehaviour    
{
    public Outliner iceController;
    private FeedbackController textController;
    public Slot[] slots;
    public Slot targetSlot;
    private void Start()
    {
        Debug.Log("IceGrid started.");
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
        Debug.Log("IceGrid started2222");
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse button released.");
            IcePlacement();
        }
    }
    
    private void IcePlacement()
    {
        Debug.Log(iceController != null);
        Debug.Log(iceController.selectedIce.name);
        Debug.Log(iceController.selectedSlot.name);
        if ((iceController.selectedIce != null) && (iceController.selectedSlot != null))
        {
            Vector3 slotPosition = iceController.selectedSlot.transform.position;
            Vector3 og_icePosition = iceController.selectedIce.transform.position;

            Vector3 newPosition = new Vector3(slotPosition.x, slotPosition.y + 0.5f, slotPosition.z);

            iceController.selectedIce.transform.position = newPosition;

            Debug.Log("Ice to slot.");
        }
    }
}