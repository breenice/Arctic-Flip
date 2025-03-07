using SQLite;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuestionFetcher : MonoBehaviour
{
    private string dbPath;
    private SQLiteConnection connection;

    void Start()
    {
        var dbPath = Application.persistentDataPath + "/ArcticFlip.db";
        // connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        connection = new SQLiteConnection(dbPath);
        //connection.CreateTable<QuestionSet>();
        CreateDB();
        Debug.Log(connection);
        Debug.Log("Connected to SQLite database at: " + dbPath);
    }
    public void CreateDB(){
        // Debug.Log("creating question db");
		connection.DropTable<QuestionSet> ();
		connection.CreateTable<QuestionSet> ();
        
        connection.DropTable<ProblemSet> ();
		connection.CreateTable<ProblemSet> ();

        connection.DropTable<SolutionSet> ();
		connection.CreateTable<SolutionSet> ();

		connection.InsertAll (new[]{
			new QuestionSet{
				Type = "Fraction",
                Difficulty = 1,
                Seen = 0
			},
            new QuestionSet{
				Type = "Fraction",
                Difficulty = 1,
                Seen = 0
			},
            new QuestionSet{
                Type = "Fraction",
                Difficulty = 2,
                Seen = 0
            },
            new QuestionSet{
                Type = "Fraction",
                Difficulty = 2,
                Seen = 0
            },
            new QuestionSet{
                Type = "Fraction",
                Difficulty = 2,
                Seen = 0
            },
            new QuestionSet{
                Type = "Fraction",
                Difficulty = 3,
                Seen = 0
            },
            new QuestionSet{
                Type = "Fraction",
                Difficulty = 3,
                Seen = 0
            },
        });
        Debug.Log("inserting problems");
        connection.InsertAll (new[]{
            new ProblemSet{
                Whole = 1,
                Half = 0,
                Third = 0,
                Forth = 0,
                Fifth = 0
            },
            new ProblemSet{
                Whole = 3,
                Half = 0,
                Third = 0,
                Forth = 0,
                Fifth = 0
            },
            new ProblemSet{
                Whole = 0,
                Half = 0,
                Third = 3,
                Forth = 0,
                Fifth = 0
            },
            new ProblemSet{
                Whole = 0,
                Half = 0,
                Third = 1,
                Forth = 0,
                Fifth = 0
            },
            new ProblemSet{
                Whole = 0,
                Half = 1,
                Third = 0,
                Forth = 0,
                Fifth = 0
            },
            new ProblemSet{
                Whole = 0,
                Half = 0,
                Third = 1,
                Forth = 2,
                Fifth = 0
            },
            new ProblemSet{
                Whole = 0,
                Half = 1,
                Third = 0,
                Forth = 0,
                Fifth = 1
            }
		});
        // Debug.Log("inserting solutions");
        connection.InsertAll (new[]{
            new SolutionSet{ // tutorial
                Whole = 1,
                Half = 0,
                Third = 0,
                Forth = 0,
                Fifth = 0
            },
            new SolutionSet{
                Whole = 3,
                Half = 0,
                Third = 1,
                Forth = 0,
                Fifth = 1
            },
            new SolutionSet{
                Whole = 1,
                Half = 1,
                Third = 1,
                Forth = 1,
                Fifth = 1
            },
            new SolutionSet{
                Whole = 1,
                Half = 0,
                Third = 1,
                Forth = 2,
                Fifth = 1
            },
            new SolutionSet{
                Whole = 1,
                Half = 0,
                Third = 1,
                Forth = 2,
                Fifth = 1
            },
            new SolutionSet{
                Whole = 3,
                Half = 1,
                Third = 1,
                Forth = 0,
                Fifth = 0
            },
            new SolutionSet{
                Whole = 1,
                Half = 0,
                Third = 1,
                Forth = 2,
                Fifth = 1
            }
        });
	}

    public int GetNextQuestion(string type, int difficulty)
    {
        // Debug.Log("getting question");
        // Debug.Log(connection.Table<QuestionSet>().ToString());
		//var question = connection.Table<QuestionData>().Where(x => x.Type == type && x.Difficulty == difficulty && x.Seen == 0).FirstOrDefault();
        //string query = "SELECT * FROM QuestionSet WHERE Type = '" + type + "' AND Difficulty = " + difficulty + " AND Seen = 0";
        var question = connection.Query<QuestionSet>("SELECT * FROM QuestionSet WHERE Type = ? AND Difficulty = ? AND Seen = 0 ORDER BY RANDOM() LIMIT 1", type, difficulty);
        if (question.Count == 0)
        {
            // Debug.Log("no more questions");
            return -1; // no more question of this type and difficulty   
        }
        else
        {
            // Debug.Log("question found");
            //question.Seen = 1;
            //connection.Update(question);
            return question[0].QuestionID;
        }
    }

    public int[] GetProblemSet(int problemId)
    {
        var problem = connection.Query<ProblemSet>("SELECT * FROM ProblemSet WHERE ProblemID = ?", problemId);

        // Debug.Log("question seen:");
        // Debug.Log("problem id: "+problemId);
        //var debug = connection.Query<QuestionSet>("SELECT * FROM QuestionSet WHERE QuestionID = ?", problemId);
        //Debug.Log(debug[0].Seen);
        if (problem.Count > 0)
        {
            // Debug.Log("problem found");
            int[] result = { problem[0].Whole, problem[0].Half, problem[0].Third, problem[0].Forth, problem[0].Fifth };
            return result;
        }
        return null; // no matching problem
    }
    public void SetProblemSeen(int problemId)
    {
        Debug.Log("updating problem");
        connection.Execute("UPDATE QuestionSet SET Seen = 1 WHERE QuestionID = ?", problemId);
        Debug.Log("set seen");
    }

    public int[] GetSolutionSet(int solutionId)
    {
        var solution = connection.Query<SolutionSet>("SELECT * FROM SolutionSet WHERE SolutionID = ?", solutionId);

        if (solution.Count > 0)
        {
            int[] result = { solution[0].Whole, solution[0].Half, solution[0].Third, solution[0].Forth, solution[0].Fifth };
            return result;
        }
        return null; // no matching solution
    }
}

// Model classes for SQLite-Net
public class QuestionSet
{
    [PrimaryKey, AutoIncrement]
    public int QuestionID { get; set; }
    public string Type { get; set; }
    public int Difficulty { get; set; }
    public int Seen { get; set; }
    public override string ToString ()
	{
		return string.Format ("[Question: Id={0}, Type={1},  Difficulty={2}, Seen={3}]", QuestionID, Type, Difficulty, Seen);
	}
}

public class ProblemSet
{
    [PrimaryKey, AutoIncrement]
    public int ProblemID { get; set; }
    public int Whole { get; set; }
    public int Half { get; set; }
    public int Third { get; set; }
    public int Forth { get; set; }
    public int Fifth { get; set; }

    public override string ToString ()
    {
        return string.Format ("[Problem: Id={0}, Whole={1}, Half={2}, Third={3}, Forth={4}, Fifth={5}]", ProblemID, Whole, Half, Third, Forth, Fifth);
    }
}

public class SolutionSet
{
    [PrimaryKey, AutoIncrement]
    public int SolutionID { get; set; }
    public int Whole { get; set; }
    public int Half { get; set; }
    public int Third { get; set; }
    public int Forth { get; set; }
    public int Fifth { get; set; }
}