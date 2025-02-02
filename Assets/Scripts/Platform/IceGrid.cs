using UnityEngine;
using System;
using System.Collections.Generic;
public class IceGrid : MonoBehaviour    
{
    public Outliner iceController;
    private FeedbackController textController;
    public GameObject[] slots = new GameObject[3];
    public Slot targetSlot;
    public GameObject[] wizardSlots = new GameObject[3];
    public GameObject[] iceStorage = new GameObject[3];
    public HashSet<int> usableIce = new HashSet<int>();

    private void Start()
    {
        Debug.Log("IceGrid started.");
        for (int i = 0; i < iceStorage.Length; i++)
        {
            usableIce.Add(i); // tells me which ice i haven't taken from storage yet
        }
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

    public void PlaceWizardIce()
    {
        System.Random random = new System.Random();
        int numIce = random.Next(1, 3);
        for (int i = 0; i < wizardSlots.Length; i++)
        {
            Vector3 oldPosition = wizardSlots[i].transform.position;
            Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y + 0.5f, oldPosition.z);
            int newIceIdx = GetRandomIce();
            GameObject newIce = iceStorage[newIceIdx];
            newIce.transform.position = newPosition;
        }
    }

    private int GetRandomIce()
    {
        System.Random random = new System.Random();
        int numIce = random.Next(1, usableIce.Count);
        usableIce.Remove(numIce);
        return numIce;
    }
}