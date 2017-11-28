using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;
using TriumObject;
using BackpackObject;
using Initialization;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
public class Quiz : MonoBehaviour {

	public Text question;
	public Text answer1;
	public Text answer2;
	public Text answer3;
	public Text answer4;
	public Backpack bp = Initialize.player;
	// Use this for initialization
	void Start () {
		getPopQuiz ();
	}
	//how will quizzes be chosen to pop up
	//-based on trium select; And rows in db
	//answers have same string as correct choice
	//tiers in backpack?
	//trium - increase tier
	//do any atoms have facts yet
	//nopeeee
	//first fact able at beginning

	public void getPopQuiz() {
		//buttonListControl.RefreshButtons();

		string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection(connectionPath);

		dbconn.Open();

		IDbCommand dbcmd = dbconn.CreateCommand();
		               
		string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.Formula, IFNULL(Trium.QuizOneID, -1), IFNULL(Trium.QuizTwoID, -1), IFNULL(Trium.QuizThreeID, -1) " + 
			"FROM Trium INNER JOIN Quiz ON Quiz.ID = ";
		//if tier blah is next, do that question
		sqlQuery = sqlQuery + "IFNULL(Trium.QuizOneID, -1) ";


		dbcmd.CommandText = sqlQuery; //+ "WHERE Trium.ID = ";//id is equal to one of the elements used?

		IDataReader reader = dbcmd.ExecuteReader();
		// reader.Read() will return True or False. If true, we will execute what is in the while() loop
		while (reader.Read ()) {
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
