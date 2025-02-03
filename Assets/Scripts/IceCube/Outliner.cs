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
using UnityEngine;
using UnityEngine.EventSystems;

public class Outliner : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    public FeedbackController feedbackController;
    public GameObject penguin;
    public GameObject selectedIce = null;
    public GameObject selectedSlot = null;
    public Vector3 selectedIce_ogPosition;

   void Start()
    {
        penguin = GameObject.Find("penguin_head");
    }
    void checkHighlight()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) // NEED EVENT SYSTEM
        {
            highlight = raycastHit.transform;
            if ((highlight.name == "ice" || highlight.CompareTag("slot")) && (highlight != selection))
            {
                //Debug.Log("Highlighted: " + highlight.tag);
                //Debug.Log(feedbackController != null);
                feedbackController.SetFeedbackText("Highlighted: " + highlight.tag);
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 20.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
    void checkSelection()
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
        checkHighlight();
        checkSelection();
    }
}
