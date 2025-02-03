using UnityEngine;
using System;
using System.Collections.Generic;
public class IceGrid : MonoBehaviour    
{
    public Outliner iceController;
    public FeedbackController textController;
    public GameObject[] slots = new GameObject[3];
    public Slot targetSlot;
    public GameObject[] wizardSlots = new GameObject[3];
    public GameObject[] iceStorage = new GameObject[3];
    private List<int> usableIce = new List<int>();
    private System.Random random = new System.Random();
    float wizardTotal = 0.0f;
    float playerTotal = 0.0f;

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
        //Debug.Log(iceController != null);
        //Debug.Log(iceController.selectedIce.name);
        //Debug.Log(iceController.selectedSlot.name);
        if ((iceController.selectedIce != null) && (iceController.selectedSlot != null))
        {
            Vector3 slotPosition = iceController.selectedSlot.transform.position;
            Vector3 og_icePosition = iceController.selectedIce.transform.position;

            Vector3 newPosition = new Vector3(slotPosition.x, slotPosition.y + 0.5f, slotPosition.z);

            iceController.selectedIce.transform.position = newPosition;
            playerTotal += getTagFraction(iceController.selectedIce.tag);

            Debug.Log("Ice to slot.");
        }
    }

    public void PlaceWizardIce()
    {
        System.Random random = new System.Random();
        int numIce = random.Next(1, 3);
        for (int i = 0; i < numIce; i++)
        {
            Vector3 oldPosition = wizardSlots[i].transform.position;
            Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y + 0.5f, oldPosition.z);
            int newIceIdx = GetRandomIce();
            GameObject newIce = iceStorage[newIceIdx];
            wizardTotal += getTagFraction(newIce.tag);
            Debug.Log(wizardTotal);
            newIce.transform.position = newPosition;
        }
    }

    private int GetRandomIce()
    {
        int pick = usableIce[random.Next(usableIce.Count)];
        usableIce.RemoveAt(pick);
        return pick;
    }
    private float getTagFraction(string tag)
    {
        Debug.Log("starting getTagFraction");
        Debug.Log(tag);
        string[] parts = tag.Split('/');
        float numerator = float.Parse(parts[0]);
        float denominator = float.Parse(parts[1]); 
        Debug.Log("num" + numerator);
        Debug.Log("denom" + denominator);
        if (denominator != 0) // no div by zero!!
            {
                Debug.Log(numerator + "/" + denominator);
                return numerator / denominator;
            }
        return 0.0f; // error handling prob
    }

    public void checkAnswer()
    {

        if (wizardTotal == playerTotal)
        {
            textController.SetFeedbackText("Correct!");
            Debug.Log(wizardTotal);
            Debug.Log(playerTotal);
        }
        else
        {
            textController.SetFeedbackText("Incorrect.");
        }
    }
}