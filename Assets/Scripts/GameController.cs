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
    public void Start()
    {
        if (startButton == null)
        {
            startButton = GameObject.Find("Start").GetComponent<Button>();
        }
        if (timerButton == null)
        {
            timerButton = GameObject.Find("start_timer").GetComponent<Button>();
        }
        startButton.onClick.AddListener(LoadScene); 
        timerButton.onClick.AddListener(StartChallenge); 
    }
    public void Update()
    {
        if (challengeActive)
        {
            // clock
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                textController.SetTime(timeLeft.ToString("0"));
            }
            else
            {
                challengeActive = false;
                // end challenge
            }
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Challenge");
    }

    public void StartChallenge()
    {
        timerButton.gameObject.SetActive(false);
        challengeActive = true;
        timeLeft = timerDuration;
    }
}
