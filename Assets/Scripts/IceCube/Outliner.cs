//MIT License
//Copyright (c) 2023 DA LAB (https://www.youtube.com/@DA-LAB)
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Outliner : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    public GameObject penguin;
    public GameObject selectedIce;
    public GameObject selectedSlot = null;
    public Vector3 selectedIce_ogPosition;

    public Raycaster raycaster;
    public string[] selectableTags = { "ice", "slot", "1/1", "1/2", "1/3", "1/4", "1/5" };

    // move mode
    public GameObject lookObj; // player looking at object
    public GameObject pickedObj; // object picked by player (on x?)

    //script
    public FeedbackController feedbackController;
    public IceGrid iceGrid;

   void Start()
    {
        penguin = GameObject.Find("penguin_head");
    }

    void checkHighlight()
    {
        if (lookObj != null)
        {
            lookObj.GetComponent<Outline>().enabled = false;
            lookObj = null;
        }
        lookObj = raycaster.getLookObject();
        Debug.Log("new: "+lookObj.name);
        if(lookObj)
        {
            if(lookObj.name == "ice") {feedbackController.SetPlayerTalk(lookObj.tag);}
            if (selectableTags.Contains(lookObj.tag) && (lookObj != pickedObj))
            {
                if (lookObj.GetComponent<Outline>() != null)
                {
                    lookObj.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = lookObj.AddComponent<Outline>();
                    outline.enabled = true;
                    lookObj.GetComponent<Outline>().OutlineColor = Color.magenta;
                    lookObj.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                lookObj = null;
            }
        }
    }
    void checkSelection()
    {
        // Selection
        if (Input.GetKey(KeyCode.X))
        {
            if (lookObj != null)
            {
                lookObj.GetComponent<Outline>().enabled = false;
            }
            if (lookObj)
            {
                pickedObj = lookObj;
                if (pickedObj.name == "ice"){ 
                    // if taken from sea
                    GameObject[]slotsFilled = iceGrid.slotsFilled;
                    if (slotsFilled.Contains(pickedObj))
                    {
                        slotsFilled[slotsFilled.ToList().IndexOf(pickedObj)] = null;
                    }

                    //if taken from player slots
                    GameObject[]playerAnswer = iceGrid.playerAnswer;
                    if (playerAnswer.Contains(pickedObj))
                    {
                        playerAnswer[playerAnswer.ToList().IndexOf(pickedObj)] = null;
                    }
                    selectedIce = pickedObj; 
                    selectedIce_ogPosition = selectedIce.transform.position;

                    if (penguin != null)
                    {
                        // Move the ice on top of the penguin
                        Vector3 penguinPosition = penguin.transform.position;

                        // Adjust the position slightly above the penguin
                        Vector3 newPosition = new Vector3(penguinPosition.x, penguinPosition.y + 2.0f, penguinPosition.z);

                        pickedObj.transform.position = newPosition;
                        pickedObj.transform.SetParent(penguin.transform);

                        //Debug.Log("Ice moved on top of the penguin.");
                    }
                    Debug.Log("Selected ice: " + selectedIce.name); 
                }
                if (pickedObj.CompareTag("slot") && selectedIce != null)
                { 
                    GameObject[]slots = iceGrid.slots;
                    GameObject[] playerAnswer = iceGrid.playerAnswer;
                    if (slots.Contains(pickedObj))
                    {
                        playerAnswer[slots.ToList().IndexOf(pickedObj)] = selectedIce;
                    }
                    selectedSlot = pickedObj.gameObject; Debug.Log("Selected slot: " + selectedSlot.name); 
                    Vector3 newPosition = selectedSlot.transform.position;
                    selectedIce.transform.position = newPosition;
                    selectedIce.transform.SetParent(null);
                }
                if(selectedSlot == null){Debug.Log("no slot");}

                feedbackController.SetFeedbackText("Selected: " + pickedObj.tag);
                pickedObj.gameObject.GetComponent<Outline>().enabled = true;
                lookObj = null;
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                    selectedIce = null;
                }
            }
        }
    }

    void checkDeselect()
    {
        if (Input.GetKeyDown(KeyCode.X) && (lookObj == null))
        {
            selectedIce.transform.SetParent(null);

            GameObject[] slots = iceGrid.usableIceSlots;
            GameObject[] slotsFilled = iceGrid.slotsFilled;
            for (int i = 0; i < slots.Length; i++)
            {
                // check if occupied
                if (slotsFilled[i] == null)
                {
                    Vector3 newPosition = slots[i].transform.position;
                    selectedIce.transform.position = newPosition;
                    slotsFilled[i] = selectedIce;
                    selectedIce = null;
                    break;
                }
            }
        }
    }
    void pointAndClickHighlight()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) // MUST HAVE EventSystem in inspector
        {
            highlight = raycastHit.transform;
            if (selectableTags.Contains(highlight.tag) && (highlight != selection))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
    void pointAndClickSelection()
    {
        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                if (selection.name == "ice"){ 
                    selectedIce = selection.gameObject; 
                    selectedIce_ogPosition = selectedIce.transform.position;

                if (penguin != null)
                {
                    // Move the ice on top of the penguin
                    Vector3 penguinPosition = penguin.transform.position;

                    // Adjust the position slightly above the penguin
                    Vector3 newPosition = new Vector3(penguinPosition.x, penguinPosition.y + 1.0f, penguinPosition.z);

                    selection.position = newPosition;

                    //Debug.Log("Ice moved on top of the penguin.");
                }
                    Debug.Log("Selected ice: " + selectedIce.name); 
                }
                if (selection.CompareTag("slot")){ selectedSlot = selection.gameObject; Debug.Log("Selected slot: " + selectedSlot.name); }
                if(selectedSlot == null){Debug.Log("no slot");}

                feedbackController.SetFeedbackText("Selected: " + selection.tag);
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                    selectedIce = null;
                }
            }
        }
    }

    void Update()
    {
        checkDeselect();
        checkHighlight();
        checkSelection();
    }
}