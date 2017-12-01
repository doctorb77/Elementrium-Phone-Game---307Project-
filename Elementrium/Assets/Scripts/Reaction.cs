using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackObject;
using TriumObject;
using Ranch;
using BudBehavior;
using Initialization;
using System.Data;
using Mono.Data.Sqlite;
using Assets.Scripts;

namespace Reaction
{

    public class ReactionHandler
    {
        public static GameObject ws = GameObject.Find("BuddyContainer");
        public static CosmicRanch cr = Initialize.ranch;
        public static Backpack bp = Initialize.player;

        public void reaction(Backpack bp, int key, int numSelected)
        {

            int fusionID = -1;

            //if (!canReact(bp, key, out fusionID))
            //{
            //    return;
            //}

            // Make sure groupID is not -1
            if (fusionID == -1)
            {
                return;
            }
        }

        public static bool reactCurrent(List<GameObject> selected, List<string> reactant2, List<int> reactantCount2, List<string> product2, List<int> productCount2)
        {
            //Debug.Log("HERE in React");
            // Check if requirements are met
            bool req = false;

			// Quizzing variables
			bool quizThem = false;
			int quizTriumID = -1;

            Debug.Log("Checkpoint A");

            if (selected.Count == 0)
            {
                Initialize.sh.setCurrentState("MainGameScene", true, true);
                return false;
            }

            Debug.Log("Checkpoint B");

            List<string> reactant = new List<string>(reactant2.ToArray());          // Reactant Names
            List<int> reactantCount = new List<int>(reactantCount2.ToArray());      // Reactant Amounts
			List<string> product = new List<string>(product2.ToArray());            // Product Names
            List<int> productCount = new List<int>(productCount2.ToArray());        // Product Amounts


            foreach (GameObject buddy in selected)
            {
                if (reactant.Contains(buddy.GetComponent<BuddyBehavior>().triumformula))
                {
                    int index = reactant.IndexOf(buddy.GetComponent<BuddyBehavior>().triumformula);

                    if (reactantCount[index] > 0)
                    {
                        reactantCount[index]--;
                    }
                    else
                    {
                        req = false;
                        Initialize.sh.setCurrentState("MainGameScene", true, true);
                        return false;
                    }
                }
                else
                {
                    // Something selected that shouldn't be
                    Initialize.sh.setCurrentState("MainGameScene", true, true);
                    return false;
                }
            }

            Debug.Log("Checkpoint C");

            req = true;

            foreach (int total in reactantCount)
            {
                if (total != 0)
                {
                    req = false;
                    Initialize.sh.setCurrentState("MainGameScene", true, true);
                    return false;
                }
            }

            Debug.Log("Checkpoint D");

            if (req)
            {
                // TESTING FOR EXP BAR : REMOVE LATER
                int reactionID = 1;
                int level = 1;
                // 1 --> Grouping
                // 2 --> Reaction
                int isGrouping = (Initialize.ranch.inGrouping) ? 1 : 2;
                Debug.Log("IsGrouping: " + isGrouping);

				// Obtain the highest experience level from the products
				List<int> productIDs = null;


				int highestLevel = getHighestLevel(product, out productIDs);
                Debug.Log("Highest Level: " + highestLevel);
                Debug.Log("Product Array: " + product.ToString());
                Debug.Log("ProductID Array: " + productIDs.ToString());
				// Something went wrong
				if (productIDs == null || highestLevel == -1)
				{
                    Debug.Log("Checkpoint E has failed!");
					Initialize.sh.setCurrentState("MainGameScene", true, true);
					return false;
				}

                Debug.Log("Checkpoint E");

				// Check to see if a product has been unlocked before
				int experienceLevel = -1;
				int newProductID = getNewlyUnlockedProduct(productIDs, out experienceLevel);
                int foundNew = 0;

				if (newProductID == -1 || experienceLevel == -1)
				{
					// We didn't make something new
				}
				else
				{
                    // We found something new!
                    foundNew = 1;
				}

				// >>>>>..    // GRAB REACTION_ID, LEVEL, and ISGROUPING

				System.Random rnd = new System.Random();

                int pick = rnd.Next(0, reactant2.Count);

                //if (!Backpack.reactionIDs.Contains(reactionID))
                if (foundNew == 1)
                {
                    bp.updateTiers(reactant);
                    string triumName = "";
                    int atomID = 1;
                    quizTriumID = getInfo(reactant2[pick], out triumName, out atomID);
                    quizThem = true;
                    Debug.Log("Quiz for " + reactant2[pick] + " with ID :" + quizTriumID);
                }

                Debug.Log("LIST : " + Backpack.reactionIDs);

				// isGrouping handles if we are in Grouping or Reaction
                Backpack.handleExp(foundNew, level, isGrouping);
                /*
                Debug.Log("*******BEGIN PRINTING BACKPACK BEFORE*******");
                foreach (Trium t in bp.getBP().Values) {
                    Debug.Log("Name: " + t.getName() + ", Count: " + t.getCount());
                }
                */
                //GameObject cr = GameObject.FindGameObjectWithTag("CosmicRanch");//.GetComponent<CosmicRanch>().AddBuddyToList();

                foreach (GameObject buddy in selected)
                {
                    string formula = buddy.GetComponent<BuddyBehavior>().triumformula;
                    //Debug.Log("Removing " + formula);

                    // TODO: CALL GETKEY TO OBTAIN THE ID OF THE TRIUM
                    string dummyString;
                    int dummyInt;
                    int key = getInfo(formula, out dummyString, out dummyInt);

                    // TODO: Error check for null
                    Trium old = bp.getTrium(key);
                    old.setCount(old.getCount() - 1);

                    cr.RemoveBuddyFromList(buddy);
                }

                foreach (string nTrium in product)
                {
                    for (int i = 0; i < productCount[product.IndexOf(nTrium)]; i++)
                    {
                        // TODO: OBTAIN THE ID OF THE TRIUM TO ADD IT TO THE BACKPACK
                        string triumName = "none";
                        int atomID = -2;
                        int key = getInfo(nTrium, out triumName, out atomID);

                        if (triumName == "none" || atomID == -2 || key == -1) {
                            //Debug.Log("ReactCurrent: PRODUCT TRIUM NOT IN DATABASE!");
                            Initialize.sh.setCurrentState("MainGameScene", true, true);
                            return false;
                        }

                        Debug.Log("Checkpoint F");

//						if (quizTriumID == -1 && bp.getTrium(key) == null) {
//							quizTriumID = key;
//							quizThem = true;
//						}

                        bp.addToBackpack(key, triumName, atomID);

                        //Debug.Log("nTrium : " + nTrium + " Path : \'\"Prefabs/Triums/" + nTrium+"\"\'");
                        GameObject buddy = Resources.Load("Prefabs/Triums/"+nTrium) as GameObject;
                        GameObject actual = GameObject.Instantiate(buddy);

                        actual.transform.SetParent(ws.transform, true);
                        //buddy.transform.SetParent(buttonTemplate.transform.parent, false);

                        float x = (float)(UnityEngine.Random.value - 0.5) * 900;
                        float y = (float)(UnityEngine.Random.value - 0.5) * 900;

                        actual.transform.localPosition = new Vector3(0, 0, -1);
                        cr.AddBuddyToList();
                    }
                }
                /*
				Debug.Log("*******BEGIN PRINTING BACKPACK AFTER*******");
				foreach (Trium t in bp.getBP().Values)
				{
					Debug.Log("Name: " + t.getName() + ", Count: " + t.getCount());
				}
                */

  


                CosmicRanch.Instance.AddBuddyToList();
                Initialize.sh.setCurrentState("MainGameScene", true, true);

				Debug.Log ("quizTriumID: " + quizTriumID);
				if (quizThem && quizTriumID != -1) {
					Initialize.quizID = quizTriumID;
                    Debug.Log("QuizID: " + Initialize.quizID);
				}

                return true;
            }
            Initialize.sh.setCurrentState("MainGameScene", true, true);



            return false;
        }

        /**
         * 
         * Utility function to obtain the database ID of the Trium
         * 
         */
         public static int getInfo(string formula, out string name, out int atomID)
         {
            name = "none";
            atomID = -1;

			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);

			dbconn.Open();

			IDbCommand dbcmd = dbconn.CreateCommand();

            string sqlQuery = "SELECT ID, Name, IFNULL(ElementID, -1) " +
                              "FROM Trium " +
                              "WHERE Formula = '" + formula + "'";
            
			dbcmd.CommandText = sqlQuery;

			IDataReader reader = dbcmd.ExecuteReader();

            int key = -1;

			// reader.Read() will return True or False. If true, we will execute what is in the while() loop
			while (reader.Read())
			{

                key = reader.GetInt32(0);
                name = reader.GetString(1);
                atomID = reader.GetInt32(2);
			}


			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;

			return key;
         }


        private static int getHighestLevel(List<string> products, out List<int> productIDs) {

            List<int> tempIDs = new List<int>();
            int highestExpLevel = -1;

            for (int i = 0; i < products.Count; i++) {

                string productName = products[i];
			    string sqlQuery = "SELECT ID, Experience " +
				"FROM Trium " +
				"WHERE Formula = '" + productName + "'";

				string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

				IDbConnection dbconn;
				dbconn = (IDbConnection)new SqliteConnection(connectionPath);
				dbconn.Open();
				IDbCommand dbcmd = dbconn.CreateCommand();
				dbcmd.CommandText = sqlQuery;

				IDataReader reader = dbcmd.ExecuteReader();

                int queryExperience = -1;
                int queryID = -1;

                while (reader.Read()) {

                    queryID = reader.GetInt32(0);
                    queryExperience = reader.GetInt32(1);

                }

				reader.Close();
				reader = null;
				dbcmd.Dispose();
				dbcmd = null;
				dbconn.Close();
				dbconn = null;

                // Make sure we got values back
                if (queryID == -1 || queryExperience == -1) {
                    Debug.Log("getHighestLevel: SCUFFED QUERY IS SCUFFED");
                    productIDs = null;
                    return -1;
                }

                // add the ID to the ProductID list
                tempIDs.Add(queryID);

                // Update the highest exp level as needed
                if (queryExperience > highestExpLevel) {
                    highestExpLevel = queryExperience;
                }

            }

            // Update product IDs and return highest exp level found
            productIDs = tempIDs;
            return highestExpLevel;

        }


        private static int getNewlyUnlockedProduct(List<int> productIDs, out int experienceLevel) {

            int unlockedID = -1;
            int unlockedExperience = -1;

            // Look through the productIDs list
            foreach (int id in productIDs)
            {
                if (Initialize.player.getTrium(id) == null) {
                    // We found something new!
                    unlockedID = id;
                    break;
                }
            }

            // Make sure we have unlocked something new
            if (unlockedID == -1) {
				experienceLevel = unlockedExperience;
				return -1;
            }


            string sqlQuery = "SELECT Experience " +
                "FROM Trium " +
                "WHERE ID = " + unlockedID;

			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);
			dbconn.Open();
			IDbCommand dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = sqlQuery;

			IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read()) {
                unlockedExperience = reader.GetInt32(0);

            }

			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;

            // Make sure we got something from the query
            if (unlockedExperience == -1) {
                experienceLevel = unlockedExperience;
                return -1;
            }

            // Return
            experienceLevel = unlockedID;
            return unlockedID;

        }

    }
}
