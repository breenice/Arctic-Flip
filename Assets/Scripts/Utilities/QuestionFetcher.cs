using SQLite4Unity3d;
using UnityEngine;
using System.Collections.Generic;

public class QuestionFetcher : MonoBehaviour
{
    private string dbPath;
    private SQLiteConnection connection;

    void Start()
    {
        dbPath = Application.persistentDataPath + "/ArcticFlip.db";
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Connected to SQLite database at: " + dbPath);
    }

    public int GetNextQuestion(string type, int difficulty)
    {
        string query = "SELECT * FROM QuestionSet WHERE Seen = 0 AND Type = ? AND Difficulty = ? ORDER BY RANDOM() LIMIT 1";
        
        var result = connection.Query<QuestionData>(query, type, difficulty);

        if (result.Count > 0)
        {
            Debug.Log($"QuestionID: {result[0].QuestionID}");
            return result[0].QuestionID;
        }
        return -1; // no more question of this type and difficulty
    }

    public int[] GetProblemSet(int problemId)
    {
        var result = connection.Query<ProblemData>("SELECT * FROM ProblemSet WHERE ProblemID = ?", problemId);

        if (result.Count > 0)
        {
            return new int[] { result[0].Whole, result[0].Half, result[0].Third, result[0].Forth, result[0].Fifth };
        }
        return null; // no matching problem
    }

    public int[] GetSolutionSet(int solutionId)
    {
        var result = connection.Query<SolutionData>("SELECT * FROM SolutionSet WHERE SolutionID = ?", solutionId);

        if (result.Count > 0)
        {
            return new int[] { result[0].Whole, result[0].Half, result[0].Third, result[0].Forth, result[0].Fifth };
        }
        return null; // no matching solution
    }
}

// Model Classes for SQLite4Unity3D
public class QuestionData
{
    [PrimaryKey, AutoIncrement]
    public int QuestionID { get; set; }
    public string Type { get; set; }
    public int Difficulty { get; set; }
    public int Seen { get; set; }
}

public class ProblemData
{
    [PrimaryKey, AutoIncrement]
    public int ProblemID { get; set; }
    public int Whole { get; set; }
    public int Half { get; set; }
    public int Third { get; set; }
    public int Forth { get; set; }
    public int Fifth { get; set; }
}

public class SolutionData
{
    [PrimaryKey, AutoIncrement]
    public int SolutionID { get; set; }
    public int Whole { get; set; }
    public int Half { get; set; }
    public int Third { get; set; }
    public int Forth { get; set; }
    public int Fifth { get; set; }
}