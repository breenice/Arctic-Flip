using UnityEngine;
using TMPro;
public class FeedbackController : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI timeText;

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
    public void SetTime(string text)
    {
        if (timeText != null)
        {
            timeText.text = text;
        }
    }
}
