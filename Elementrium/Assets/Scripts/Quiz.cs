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


namespace Assets.Scripts {
	public class Quiz : MonoBehaviour
	{
		public Animator QuizAnim;
		public GameObject QuizSystem;
		public bool quizOn;
		public Text question;
		public Text answer1;
		public Text answer2;
		public Text answer3;
		public Text answer4;
		public Button buttonA;
		public Button buttonB;
		public Button buttonC;
		public Button buttonD;
		public Button okay;
		public string rightanswer;
		public Backpack bp = Initialize.player;
		// Use this for initialization
		void Start()
		{
			//Debug.Log ("buttonA in start:" + Initialize.quizzer.buttonA.name);
			QuizAnim = QuizSystem.GetComponent<Animator>();
			quizOn = false;
            RefreshQuiz();
			//quizUser ();
		}
		//how will quizzes be chosen to pop up
		//-based on trium select; And rows in db
		//answers have same string as correct choice
		//tiers in backpack?
		//trium - increase tier
		//do any atoms have facts yet
		//nopeeee
		//first fact able at beginning

		public void getPopQuiz()
		{
			//buttonListControl.RefreshButtons();

			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);

			dbconn.Open();

			IDbCommand dbcmd = dbconn.CreateCommand();

			string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.Formula, IFNULL(Trium.QuizOneID, -1), IFNULL(Trium.QuizTwoID, -1), IFNULL(Trium.QuizThreeID, -1) " +
				"FROM Trium INNER JOIN Quiz ON ID = ";
			//if tier blah is next, do that question
			sqlQuery = sqlQuery + "IFNULL(Trium.QuizOneID, -1) ";


			dbcmd.CommandText = sqlQuery; //+ "WHERE Trium.ID = ";//id is equal to one of the elements used?

			IDataReader reader = dbcmd.ExecuteReader();
			// reader.Read() will return True or False. If true, we will execute what is in the while() loop
			while (reader.Read())
			{

			}
		}

		// Update is called once per frame
		void Update()
		{
			if (Initialize.quizID != -1) {
				Debug.Log ("WE MADE IT DAD");
				quizUser ();
			}

		}

        /**
         * quizUser
         * 
         * Method to begin the quizzing process. Obtains necessary information 
         * to feed to the Quiz UI.
         */
        public void quizUser() 
        {
			
			Debug.Log ("WE BE QUIZZIN!");
			Debug.Log ("THE ID IS: " + Initialize.quizID);
			okay.gameObject.SetActive(false);

			if (Initialize.quizID == -1) {
				return;
			}

			int id = Initialize.quizID;
			Initialize.quizID = -1;

			Debug.Log ("THE ID IS VALID: " + Initialize.quizID);


			if (!quizOn) {
				QuizAnim.Play ("QuizEnter");
				quizOn = true;
				Initialize.sh.setCurrentState ("Quiz", true, true);
				// validate input & verify they have unlocked this Trium
				if (id < 1 || Initialize.player.getTrium (id) == null) {
					return;
				}

				int quizNum = Random.Range (1, 3);
				string columnName = null;

				// Update the column name we want to use
				switch (quizNum) {
				case 1:
					columnName = "QuizOneID";
					break;
				case 2:
					columnName = "QuizTwoID";
					break;
				case 3:
					columnName = "QuizThreeID";
					break;
				}

				// Make sure the column name was updated
				if (columnName == null) {
					return;
				}

				/**
             * SQL Query to obtain:
             * 
             * Trium ID
             * Trium Name
             * Quiz ID
             * Quiz Question
             * Quiz ChoiceA
             * Quiz ChoiceB
             * Quiz ChoiceC
             * Quiz ChoiceD
             * Quiz Answer
             * 
             * 9 columns total
             */
				string sqlQuery = "SELECT t.ID AS TriumID, t.Name, q.ID, q.Question, q.ChoiceA, q.ChoiceB, IFNULL(q.ChoiceC, 'defaultValue'), IFNULL(q.ChoiceD, 'defaultValue'), q.Answer " +
				                 "FROM Trium t " +
				                 "INNER JOIN Quiz q ON q.ID = t." + columnName + " " +
				                 "WHERE t.ID = " + id;

				string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";
				IDbConnection dbconn;
				dbconn = (IDbConnection)new SqliteConnection (connectionPath);
				dbconn.Open ();
				IDbCommand dbcmd = dbconn.CreateCommand ();
				dbcmd.CommandText = sqlQuery;

				// Set up local variables & set default values
				// "defaultValue"
				int triumID = -1;
				string defaultValue = "defaultValue";
				string triumName = defaultValue;
				int quizID = -1;
				string questionString = defaultValue;
				string choiceA = defaultValue;
				string choiceB = defaultValue;
				string choiceC = defaultValue;
				string choiceD = defaultValue;
				rightanswer = defaultValue;

				IDataReader reader = dbcmd.ExecuteReader ();
				while (reader.Read ()) {
					triumID = reader.GetInt32 (0);
					triumName = reader.GetString (1);
					quizID = reader.GetInt32 (2);
					questionString = reader.GetString (3);
					choiceA = reader.GetString (4);
					choiceB = reader.GetString (5);
					choiceC = reader.GetString (6);
					choiceD = reader.GetString (7);
					rightanswer = reader.GetString (8);

				}

				// Verify we got values back from the query
				if (triumID == -1 || quizID == -1) {
					return;
				}
				if (triumName == defaultValue || questionString == defaultValue ||
				            choiceA == defaultValue || choiceB == defaultValue ||
				            rightanswer == defaultValue) {
					return;
				}

				// Close database objects for memory purposes
				reader.Close ();
				reader = null;
				dbcmd.Dispose ();
				dbcmd = null;
				dbconn.Close ();
				dbconn = null;


				/* kate do  your magic here pls <3 */
				question.text = questionString;
				buttonA.gameObject.SetActive (true);
				buttonB.gameObject.SetActive (true);
				if (choiceC == defaultValue) {
					// This is a true/false question
					//boxes C and D are invisible
					buttonC.gameObject.SetActive (false);
					buttonD.gameObject.SetActive (false);
					answer1.text = choiceA;
					answer2.text = choiceB;
					answer3.text = "";
					answer4.text = "";

				} else {
					buttonC.gameObject.SetActive (true);
					buttonD.gameObject.SetActive (true);
					answer1.text = choiceA;
					answer2.text = choiceB;
					answer3.text = choiceC;
					answer4.text = choiceD;
					// This is a normal question
				}
						
			}
        }

        private void random(int lower, int upper) {
            
        }

		public void OnClick(string picked) {
			Debug.Log ("IT WAS CLICKED");
			Debug.Log ("Picked: " + picked);
			okay.gameObject.SetActive (true);
			Debug.Log ("okay is: " +okay.IsActive());
			if (picked == "A") {
				if (rightanswer == answer1.text) {
                    //got it right make green
                    buttonA.GetComponent<Image>().color = Color.green;
				} else {
					//make red then
                    buttonA.GetComponent<Image>().color = Color.red;
					if (rightanswer == answer2.text) {
						//make green
                        buttonB.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer3.text) {
						//make green
                        buttonC.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer4.text) {
						//make green
                        buttonD.GetComponent<Image>().color = Color.green;
					}
				}
			}
			else if (picked == "B") {
				if (rightanswer == answer2.text) {//got it right
					//make green
                    buttonB.GetComponent<Image>().color = Color.green;
				} else {
					//make red then
                    buttonB.GetComponent<Image>().color = Color.red;
					if (rightanswer == answer1.text) {
						//make green
                        buttonA.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer3.text) {
						//make green
                        buttonC.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer4.text) {
						//make green
                        buttonD.GetComponent<Image>().color = Color.green;
					}
				}
			}
			else if (picked == "C") {
				if (rightanswer == answer3.text) {//got it right
					//make green
                    buttonC.GetComponent<Image>().color = Color.green;
				} else {
					//make red then
                    buttonC.GetComponent<Image>().color = Color.red;
					if (rightanswer == answer1.text) {
						//make green
                        buttonA.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer2.text) {
						//make green
                        buttonB.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer4.text) {
						//make green
                        buttonD.GetComponent<Image>().color = Color.green;
					}
				}
			}
			else if (picked == "D") {
				if (rightanswer == answer4.text) {//got it right
					//make green
                    buttonD.GetComponent<Image>().color = Color.green;
				} else {
					//make red then
                    buttonD.GetComponent<Image>().color = Color.green;
					if (rightanswer == answer1.text) {
						//make green
                        buttonA.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer2.text) {
						//make green
                        buttonB.GetComponent<Image>().color = Color.green;
					} else if (rightanswer == answer3.text) {
						//make green
                        buttonC.GetComponent<Image>().color = Color.green;
					}
				}
			}
			//okay.gameObject.SetActive (true);
			//Debug.Log ("okay is: " +okay.IsActive());
		}

		public void ExitQuiz() {//click on "OkButton"
			//Vinson makes it exit by summoning the Lord Gustavo
			if (quizOn) {
				QuizAnim.Play ("QuizExit");
				quizOn = false;
                RefreshQuiz();
				Initialize.sh.setCurrentState ("MainGameScene", true, true);
			}
		}

        public void RefreshQuiz() {
			okay.gameObject.SetActive(false);
			buttonA.GetComponent<Image>().color = Color.white;
			buttonB.GetComponent<Image>().color = Color.white;
			buttonC.GetComponent<Image>().color = Color.white;
			buttonD.GetComponent<Image>().color = Color.white;
        }

	}

}
