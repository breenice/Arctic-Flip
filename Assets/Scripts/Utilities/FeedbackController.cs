using UnityEngine;
using TMPro;
public class FeedbackController : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;

    void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "feedback Initialized!";
        }
    }

    void Update()
    {
        // if (feedbackText != null)
        // {
        //     // feedbackText.text = "Mouse Position: " + Input.mousePosition.ToString();

        // }
    }

    public void SetFeedbackText(string text)
    {
        if (feedbackText != null)
        {
            feedbackText.text = text;
        }
    }
}
