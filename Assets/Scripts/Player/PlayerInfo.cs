using NUnit.Framework.Internal;
using SQLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private SQLiteConnection connection;
    [SerializeField] public TMP_InputField input;
    [SerializeField] public TextMeshProUGUI username;

    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI wins;
    [SerializeField] private TextMeshProUGUI wizardTalk;
    private int playerLevel = 1;
    private int playerPoints = 0;
    PlayerSet player = null;
    [SerializeField] private SoundFX soundFX;
    void Start()
    {
        username.gameObject.SetActive(false);
        var dbPath = Application.persistentDataPath + "/PlayerInfo.db";
        connection = new SQLiteConnection(dbPath);
        CreateDB();
        Debug.Log(connection);
        Debug.Log("Connected to Player SQLite database at: " + dbPath);
    }
    public void wrongAnswer(){
        soundFX.PlaySound("wrong");
        wizardTalk.text = "hehe\n>:)";
    }
    public int getLevel(){
        return playerLevel;
    }
    public void addPoints(){
        wizardTalk.text = "no!\n>:(";
        soundFX.PlaySound("point");
        Debug.Log("here");
        playerPoints += 1 * playerLevel;
        points.text = "Points : " + playerPoints;
        switch (playerPoints)
        {
            case 2:
                soundFX.PlaySound("lvlUp");
                playerLevel = 2;
                break;
            case 8:
                soundFX.PlaySound("lvlUp");
                playerLevel = 3;
                break;
            case 14:
                soundFX.PlaySound("lvlUp");
                playerLevel = 4;
                break;
        }
        level.text = "Level : " + playerLevel;
    }
    public void resetWizard()
    {
        string[] response = {"more!", "huh??", "uh oh\nD:"};
        wizardTalk.text = response[playerLevel-1];    
    }
    public void CreateDB(){
        Debug.Log("creating player db");
		connection.CreateTable<PlayerSet> ();
        // Debug.Log("created player table");
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("checking now.." + input.text);
            // userExists(username.text);
            input.gameObject.SetActive(false);
            username.gameObject.SetActive(true);
            username.text = input.text;
        } 
    }

    public void CreatePlayer(string username){
        connection.Insert(new PlayerSet{
            Username = username,
            Points = 0,
            Wins = 0,
            Level = 1
        });
        Debug.Log("creating player");
    }
    public void userExists(string username){
        var player = connection.Query<PlayerSet>("SELECT * FROM PlayerSet WHERE Username = ?", username);
        if (player.Count == 0)
        {
            // creates new if not exist
            CreatePlayer(username);
        }
        // update all ui info
        updateUI(username);
    }
    void updateUI(string username){
        var sol = connection.Query<PlayerSet>("SELECT * FROM PlayerSet WHERE Username = ?", username);
        points.text = "Points : " + sol[0].Points;
        level.text ="Level : " + sol[0].Level;
        wins.text = "Solved: " + sol[0].Wins;
    }
    public void addWin(string username, int level)
    {
        connection.Query<PlayerSet>("UPDATE PlayerSet SET Points = Points + 1, Wins = Wins + 1 WHERE Username = ?", username);
        var sol = connection.Query<PlayerSet>("SELECT * FROM PlayerSet WHERE Username = ?", username);
        connection.Query<PlayerSet>("UPDATE PlayerSet SET Level = ? WHERE Username = ?", level, username);
        updateUI(username);
    }
}

// Model Classes for SQLite with Player info
public class PlayerSet
{
    [PrimaryKey, AutoIncrement]
    public int PlayerID { get; set; }
    public string Username { get; set; }
    public int Points { get; set; }
    public int Wins { get; set; }
    public int Level { get; set; }
    public override string ToString ()
	{
		return string.Format ("PlayerID={0}, Type={1},  UserID={2}, Points={3}, Wins={4}, Level={5}]", PlayerID, Username, Points, Wins, Level);
	}
}