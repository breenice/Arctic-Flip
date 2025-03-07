using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button timerButton;
    public Button menuButton;
    public Button exitButton;
    public FeedbackController textController;
    public float timerDuration = 120f; // 2 minutes
    public float timeLeft;
    public bool challengeActive = false;
    public IceGrid iceGrid;
    private bool enableReset = false;  

    // script
    public PlayerInfo playerInfo;
    public PlayerController playerController;
    public PlatformController platformController;
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
        menuButton.onClick.AddListener(toMainMenu);
        exitButton.onClick.AddListener(Application.Quit);
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
                bool correct = iceGrid.checkAnswer();
                Debug.Log("incorrect");
                if (correct) 
                { 
                    playerInfo.addWin(playerInfo.username.text, iceGrid.level);
                    textController.SetTime("Time's up! Correct!");
                }
                else
                {
                    Debug.Log("you're flipped!");
                    platformController.StartFlip();
                    textController.SetTime("Time's up! Incorrect!");
                }
                timerButton.gameObject.SetActive(true);
                enableReset = true;
                timerButton.onClick.AddListener(StartChallenge); 
                timerButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Again?";
                Debug.Log(playerInfo.username.text);
            }
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Challenge");
    }

    public void StartChallenge()
    {
        platformController.resetPlatform(enableReset);
        iceGrid.resetIceAssets(enableReset);
        textController.SetTime("Ready");
        System.Threading.Thread.Sleep(1);
        textController.SetTime("Set...");
        System.Threading.Thread.Sleep(1);
        textController.SetTime("Go!");
        timerButton.onClick.RemoveListener(StartChallenge); 
        timerButton.gameObject.SetActive(false);
        challengeActive = true;
        timeLeft = timerDuration + 0.1f;
        // generate ices for prob
        iceGrid.PlaceIce();
    }
    public void toMainMenu()
    {
        Debug.Log("loading menu scene");
        SceneManager.LoadScene("Main_Menu");
    }
}
