using UnityEngine;
using TMPro;
public class FeedbackController : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI playerTalk;
    public TextMeshProUGUI wizardTalk;

    void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "feedback Initialized!";
        }
        if (timeText != null)
        {
            timeText.text = "game start!";
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
    public void SetPlayerTalk(string text)
    {
        if (playerTalk != null)
        {
            playerTalk.text = text;
        }
    }
    public void SetWizardTalk(string text)
    {
        if (wizardTalk != null)
        {
            wizardTalk.text = text;
        }
    }
}
