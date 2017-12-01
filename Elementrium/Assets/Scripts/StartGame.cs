using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;
using BackpackObject;
using TriumObject;
using WormholeObject;
using GlossaryObject;
using Ranch;
using Assets.Scripts;
using System.Data;
using Mono.Data.Sqlite;


namespace Initialization
{

    public class StartGame : MonoBehaviour
    {
       
        public void EnterGame()
        {
            SceneManager.LoadScene("MainGameScene");
            

            Initialize.onStart();
        }


    }

    public static class Initialize
    {
        public static StateHandler sh;
        public static Backpack player;
        // public static Wormhole wormhole;
        public static GameObject wormhole;
        public static Glossary glossary;
        public static CosmicRanch ranch;
        public static List<Buddy> buddyList;
        public static List<string> eFormulas;  // List of element formulas
        public static List<string> eNames;     // List of element names
		//public static Quiz quizzer;
		public static int quizID;

        // Other global variables go here


        public static void onStart()
        {
            // Initialize Backpack
            if (player == null) {
                initBackpack();
            }


            // Initialize Wormhole
            if (wormhole == null) {
               wormhole = new GameObject("wormhole"); 
            }

            //testHole th = new testHole(player);
            //wormhole.AddComponent(th.GetType());

            // Initialize Glossary
            //glossary = new Glossary(player);

            // Initialize CosmicRanch
            if (ranch == null) {
                ranch = new CosmicRanch();
            }

            if (buddyList == null) {
                buddyList = new List<Buddy>();
            }


            if (sh == null)
            {
                sh = new StateHandler();
                sh.setCurrentState("MainGameScene", true, true);
            }

            /*
			if (quizzer == null) 
			{
				//Debug.Log ("buttonA:" + Initialize.quizzer.buttonA.name);
				quizzer = new Quiz();
				//Debug.Log ("buttonA:" + Initialize.quizzer.buttonA.name);
			}
			*/

            //if (eFormulas == null || eNames == null) {

                eFormulas = new List<string>();
                eNames = new List<string>();

                string sqlQuery = "SELECT Name, Formula " +
                    "FROM Trium " +
                    "WHERE ElementID not null";

                IDataReader reader = SQLiteExample.makeQuery(sqlQuery);



                while (reader.Read()) {
                    eNames.Add(reader.GetString(0));
                    eFormulas.Add(reader.GetString(1));
                }

                Debug.Log("eNamesLen: " + eNames.Count + ", eFormulasLen: " + eFormulas.Count);

                
           // }

			quizID = -1;


            // Initialize ANY other global variables

        }

        private static void initBackpack() {
            // do stuff

            // TODO: Check if there is a saved data file

            // default backpack init
            player = new Backpack();



        }

    }

}
