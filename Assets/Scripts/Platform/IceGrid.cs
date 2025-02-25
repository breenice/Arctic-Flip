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
    public List<GameObject> iceStorage = new List<GameObject>();
    public GameObject[] usableIceSlots = new GameObject[8];
    public GameObject[] slotsFilled = new GameObject[8];
    private System.Random random = new System.Random();
    float wizardTotal = 0.0f;
    float playerTotal = 0.0f;
    public QuestionFetcher questionFetcher;

    private void Start()
    {
        Debug.Log("IceGrid started.");
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse button released.");
            // IcePlacement();
        }
    }

    /**
    *  for point and click game mode
    **/
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

    public void PlaceIce()
    {
        int questionId = questionFetcher.GetNextQuestion("Fraction", 1);
        textController.SetFeedbackText(questionId.ToString());
        int[] problemSet = questionFetcher.GetProblemSet(questionId);
        PlaceWizardIce(problemSet);
        int[] solutionSet = questionFetcher.GetSolutionSet(questionId);
        PlaceSolutionIce(solutionSet);
    }

    private void PlaceWizardIce(int[] problemSet)
    {
        Debug.Log("placing wizard ice");
        int currIceSlot = 0;
        for (int i = 0; i < problemSet.Length; i++)
        {
            int numIce = problemSet[i]; // number of ice of this type to place
            while(numIce > 0)
            {
                GameObject newIce = GetMatchingIce(i);
                Debug.Log("ice picked: " + newIce.name);
                Vector3 oldPosition = wizardSlots[currIceSlot].transform.position;
                Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y + 0.5f, oldPosition.z);
                newIce.transform.position = newPosition; // move ice to slot
                wizardTotal += getTagFraction(newIce.tag); // incerement wizard ice block fracion total

                numIce -= 1;
                currIceSlot += 1;
                Debug.Log("num ice placed: " + numIce);
            }
            // end when no more problem ice to place
        }
    }

    private void PlaceSolutionIce(int[] solutionSet)
    {
        int currIceSlot = 0;
        for(int i = 0; i < solutionSet.Length; i++)
        {
            int numIce = solutionSet[i];
            while(numIce > 0)
            {
                GameObject newIce = GetMatchingIce(i);
                Vector3 oldPosition = usableIceSlots[currIceSlot].transform.position;
                Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y + 0.5f, oldPosition.z);
                newIce.transform.position = newPosition; // move new ice to usable ice slot

                numIce -= 1;
                currIceSlot += 1;
            }
        }
    }

    private void RandIcePlacement()
    {
        // RANDOM ICE PLACEMENT IMPLEMENTATION 
        // for random free play
        System.Random random = new System.Random();
        int numIce = random.Next(1, 3);
        for (int i = 0; i < numIce; i++)
        {
            Vector3 oldPosition = wizardSlots[i].transform.position;
            Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y + 0.5f, oldPosition.z);
            int newIceIdx = random.Next(0, iceStorage.Count);
            GameObject newIce = iceStorage[newIceIdx];
            wizardTotal += getTagFraction(newIce.tag);
            Debug.Log(wizardTotal);
            newIce.transform.position = newPosition;
        }
        Debug.Log("placing wizard ice");
    }

    private GameObject GetMatchingIce(int idx)
    {
        for (int i=0; i < iceStorage.Count; i++)
        {
            string tag = "1/"+(idx+1);
            Debug.Log(tag);
            GameObject pick = iceStorage[i];
            // could make mult lists per type but seems bloated bc only 3 ice per type
            // Debug.Log("ice stoage tag: " + iceStorage[i].tag);
            // Debug.Log(iceStorage[i].tag.Equals(tag));
            if (iceStorage[i].tag.Equals(tag))
            {
                iceStorage.RemoveAt(i);
                Debug.Log("ice picked: " + pick.name);
                return pick;
            }
        }
        return null;
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