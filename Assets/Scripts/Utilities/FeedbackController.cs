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
        if (feedbackText != null)
        {
            feedbackText.text = "Mouse Position: " + Input.mousePosition.ToString();

        }
    }
}
