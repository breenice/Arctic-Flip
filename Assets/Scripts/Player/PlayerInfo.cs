using SQLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private SQLiteConnection connection;
    public TMP_InputField username;

    public TextMeshProUGUI points;
    public TextMeshProUGUI level;
    public TextMeshProUGUI wins;
    PlayerSet player = null;
    void Start()
    {
        var dbPath = Application.persistentDataPath + "/PlayerInfo.db";
        connection = new SQLiteConnection(dbPath);
        CreateDB();
        Debug.Log(connection);
        Debug.Log("Connected to Player SQLite database at: " + dbPath);
    }
    public void CreateDB(){
        // Debug.Log("creating db");
		connection.DropTable<PlayerSet> ();
		connection.CreateTable<PlayerSet> ();
        Debug.Log("created player table");
        Debug.Log("inserting solutions");
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("checking now.." + username.text);
            userExists(username.text);
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
        switch (sol[0].Points)
        {
            case < 5:
                level = 1;
                break;
            case < 10:
                level = 2;
                break;
            default:
                level = 3;
                break;
        }
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