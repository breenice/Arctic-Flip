using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button timerButton;
    public FeedbackController textController;
    public float timerDuration = 120f; // 2 minutes
    public float timeLeft;
    public bool challengeActive = false;
    public IceGrid iceGrid;
    public void Start()
    {
        if (startButton == null)
        {
            startButton = GameObject.Find("Start").GetComponent<Button>();
        }
        startButton.onClick.AddListener(LoadScene); 
        if (timerButton == null)
        {
            timerButton = GameObject.Find("start_timer").GetComponent<Button>();
        }
        timerButton.onClick.AddListener(StartChallenge); 
    }
    public void Update()
    {
        // textController.SetFeedbackText("Timer: " + timeLeft.ToString("0"));
        if (challengeActive)
        {
            // clock
            if (timeLeft > 1)
            {
                timeLeft -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(timeLeft / 60);
                int seconds = Mathf.FloorToInt(timeLeft % 60);
                string timeFormatted = $"{minutes:00}:{seconds:00}";
                textController.SetTime(timeFormatted);
            }
            else
            {
                challengeActive = false;
                textController.SetTime("Time's up!");
                iceGrid.checkAnswer();
            }
            if (timerButton.gameObject.activeSelf)
            {
                Debug.LogError("timerButton reactivated unexpectedly!");
            }
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Challenge");
    }

    public void StartChallenge()
    {
        timerButton.onClick.RemoveListener(StartChallenge); 
        timerButton.gameObject.SetActive(false);
        challengeActive = true;
        timeLeft = timerDuration;
        // generate ices for prob
        iceGrid.PlaceWizardIce();
    }
}
