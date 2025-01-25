using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public void Start()
    {
        if (startButton == null)
        {
            startButton = GameObject.Find("Start").GetComponent<Button>();
        }
        startButton.onClick.AddListener(StartGame); 
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Challenge");
    }
}
