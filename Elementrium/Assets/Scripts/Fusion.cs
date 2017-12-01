using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BackpackObject;
using TriumObject;
using Ranch;
using Initialization;
using Assets.Scripts;
using Mono.Data.Sqlite;
using System.Data;
using System;
using BudBehavior;

namespace Fusion
{

	public class FusionHandler
	{
        // Put this here because I am too lazy to figure out the database thing right now. Handles up to Fl
        //public List<string> eList = new List<string> { "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar" };
        //public List<string> eName = new List<string> { "Hydrogen", "Helium", "Lithium", "Berylium", "Boron", "Carbon", "Nitrogen", "Oxygen", "Flourine" };
        public List<string> eList = Initialize.eFormulas;
        public List<string> eName = Initialize.eNames;
        public static GameObject ws = GameObject.Find("BuddyContainer");
		public CosmicRanch cr = Initialize.ranch;
		public Backpack bp = Initialize.player;

        /**
         * fuse()
         * 
         * Algorithm used to "fuse" atoms within a User's backpack. 
         * 
         */
        public bool fuse(List<GameObject> selected)
        {

            /* TODO: Support selecting multiple atoms at a time
            if (keyOne != keyTwo) {
                return;
            }
            */

            int maxFuse = Backpack.maxElement;

            // Make sure the list isn't empty
            if (selected.Count != 2)
            {
                Initialize.sh.setCurrentState("MainGameScene", true, true);
                return false;
            }
            int Trium1ID = selected[0].GetComponent<BuddyBehavior>().TriumID;
            int Trium2ID = selected[1].GetComponent<BuddyBehavior>().TriumID;

            //Debug.Log(Trium1ID);
            //Debug.Log(Trium2ID);

            if (Trium1ID <= 0 || Trium2ID <= 0 || Trium1ID > 92 || Trium2ID > 92) // One of the Triums is not an Element
            {
                Initialize.sh.setCurrentState("MainGameScene", true, true);
                return false;
            }
            int comb = Trium1ID + Trium2ID;

            // LEVEL NOT HIGH ENOUGH FOR THIS FUSION
            if (comb > maxFuse)
            {
                Initialize.sh.setCurrentState("MainGameScene", true, true);
                return false;
            }

            //Debug.Log("ATOM ID FOR FUSION : " + comb);

            int fusionID = comb;  // The database ID of the trium
            string atomName = eName[comb - 1];  // The name of the trium
            string formula = eList[comb - 1];  // The Formula of the trium
            int atomicNumber = comb;  // The Atomic Number of the trium

			// Get experience level
			int experienceLevel = getExpLevel(comb);
			if (experienceLevel == -1)
			{
				Initialize.sh.setCurrentState("MainGameScene", true, true);
				return false;
			}

            /****** Update backpack and remove the selected buddies ******/
            // Destroy old Triums
            Trium old = (Trium)bp.getBP()[Trium1ID];
            old.setCount(old.getCount() - 1);
            Trium old2 = (Trium)bp.getBP()[Trium2ID];
            old.setCount(old2.getCount() - 1);

            GameObject cr = GameObject.FindGameObjectWithTag("CosmicRanch");//.GetComponent<CosmicRanch>().AddBuddyToList()

            foreach (GameObject b in selected)
            {
                //Debug.Log("Removing " + b.GetComponent<BuddyBehavior>().triumformula);
                cr.GetComponent<CosmicRanch>().RemoveBuddyFromList(b);
            }

            // See if we've unlocked this Trium before.

            Backpack.unlockedElement[comb] = true;
			/****** Update backpack and add new GameObject buddy ******/

            // Add new Trium to backpack
           // Get rid of any spaces in formula
            formula = formula.Replace(" ", "");

			// Create a buddy game object from existing Prefabs

			// USE DATABASE QUERY HERE IN PLACE OF ELIST IF YOU WANT
			if (atomicNumber - 1 > -1)
			{
				//Debug.Log("CREATED" + atomicNumber);
				GameObject buddy = TriumGODispenser.dispenserList[atomicNumber - 1];
				//GameObject buddy = Resources.Load("Prefabs/Triums/" + eList[comb-1]) as GameObject;

				// Instantiate an actual game object and transform it on the screen
				GameObject actual = GameObject.Instantiate(buddy);
				actual.transform.SetParent(ws.transform, true);
				//buddy.transform.SetParent(buttonTemplate.transform.parent, false);
				float x = (float)(UnityEngine.Random.value - 0.5) * 900;
				float y = (float)(UnityEngine.Random.value - 0.5) * 900;
				actual.transform.localPosition = new Vector3(0, -430, -1);

				// Create a new buddy object and add it to the cosmic ranch
				Buddy bud = new Buddy(0, x, y, atomicNumber, atomName, actual, false, false);
				cr.GetComponent<CosmicRanch>().AddBuddyToList();
			}
            //cr.GetComponent<CosmicRanch>().AddBuddyToList(actual); 
            Initialize.sh.setCurrentState("MainGameScene", true, true);

            bool quizThem = (bp.getTrium(comb) == null);
            System.Random rnd = new System.Random();
            int elementToQuiz = 1;
            int pick = 0;
            if (Backpack.fusionIDs.Count != 0)
            {
                pick = rnd.Next(Backpack.fusionIDs.Count);
                elementToQuiz = Backpack.fusionIDs[pick];
            }

			// This is a new Trium, let's do a quiz! :) 
            if (quizThem) {
				//Debug.Log ("buttonA:" + Initialize.quizzer.buttonA.name);
				//Debug.Log ("I MADE IT MOM");
				Initialize.quizID = elementToQuiz;
			}

            //>>>>>...// Grab LEVEL


            int level = 1;
            // printList("Fusion ID Pre", Backpack.fusionIDs);
            //Debug.Log("COMB: " + comb + "\tEXPLEVEL: " + experienceLevel);
            Backpack.handleExp(comb, experienceLevel, 0);
            List<int> twoFus = new List<int>();
            if (Trium1ID == Trium2ID)
                twoFus.Add(Trium1ID);
            else
            {
                twoFus.Add(Trium1ID);
                twoFus.Add(Trium2ID);
            }

            bp.updateTiers(twoFus);
            // printList("Fusion ID Post", Backpack.fusionIDs);
            bp.addToBackpack(comb, atomName, atomicNumber);
            
            return true;

		}


		/**
         * canFuse()
         * 
         * Utility method to ensure that the User can indeed fuse two of the same atom.
         * 
         * Updates output variables upon valid Fusion attempts. 
         *
         */
		public bool canFuse(int key, out int fusionID, out string name, out string formula, out int AtomicNumber)
		{

			// Set initial value of "out" variables
			fusionID = -1;
			name = "none";
			formula = "none";
			AtomicNumber = -1;

			// Check to make sure the given key is a valid key
			if (!bp.getBP().ContainsKey(key))
			{
				return false;
			}

			// Obtain the corresponding Trium
			Trium t = (Trium)bp.getBP()[key];

			// Check that the Trium is an atom
			if (t.getAtomicNumber() == -1)
			{
				return false;
			}

			// Check to see they have at least two of the given Trium (atom)
			if (t.getCount() < 2)
			{
				return false;
			}

			int oldElementID = t.getAtomicNumber();


			// TODO: Check to see that they have the level capability to fuse


			// Set up string to Query from database
			string query = "SELECT Trium.ID, Trium.Name, Trium.Formula, Element.AtomicNumber " +
						   "FROM Trium" +
						   "INNER JOIN Element e ON IFNULL(Trium.ElementID, -1) = Element.ID " +
						   "WHERE Element.AtomicNumber = " + (oldElementID + 1);

			// Create new connection to database
			string connection = "URI = file:" + Application.dataPath + "/Elementrium.db";
			IDbConnection dbConn = (IDbConnection)new SqliteConnection(connection);

			// Open the connection
			dbConn.Open();

			// Set up new command query
			IDbCommand dbCmd = dbConn.CreateCommand();
			dbCmd.CommandText = query;

			// Execute query
			IDataReader dbReader = dbCmd.ExecuteReader();

			while (dbReader.Read())
			{
				fusionID = dbReader.GetInt32(0);
				name = dbReader.GetString(1);
				formula = dbReader.GetString(2);
				AtomicNumber = dbReader.GetInt32(3);

				break;
			}

			if (fusionID == -1 || name == "none" || formula == "none" || AtomicNumber == -1)
			{
				return false;
			}

			// Close database connections
			dbReader.Close();
			dbCmd.Dispose();
			dbConn.Close();

			return true;

		}

        /**
         * obtainAtomID
         * 
         * Helper method to obtain the database ID of the atom we want to fuse.
         */
		public int obtainAtomID(GameObject buddy)
        {

            string name = buddy.GetComponent<BuddyBehavior>().triumformula;

            string query = "SELECT t.ID" +
						   "FROM Trium t " +
						   "WHERE t.formula = " + name;

			// Create new connection to database
			string connection = "URI = file:" + Application.dataPath + "/Elementrium.db";
			IDbConnection dbConn = (IDbConnection)new SqliteConnection(connection);

			// Open the connection
			dbConn.Open();

			// Set up new command query
			IDbCommand dbCmd = dbConn.CreateCommand();
			dbCmd.CommandText = query;

			// Execute query
			IDataReader dbReader = dbCmd.ExecuteReader();

            int id = -1;

			while (dbReader.Read())
			{

                id = dbReader.GetInt32(0);

				break;

			}

			if (id == -1)
			{
				return -1;
			}

			// Close database connections
			dbReader.Close();
			dbCmd.Dispose();
			dbConn.Close();


            return id;

		}

        public void printList(string name, List<int> s)
        {
            for (int i = 0; i < s.Count; i++)
            {
                Debug.Log("[" + name + "] " + i + " : " + s[i]);
            }
        }

        /**
         * 
         * Gets the experience level of Trium with the given ID
         * 
         */
        public static int getExpLevel(int id) {
            int exp = -1;

            string sqlQuery = "SELECT IFNULL(Experience, -1) " +
                "FROM Trium " +
                "WHERE ID = " + id;

			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);
			dbconn.Open();
			IDbCommand dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = sqlQuery;

			IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read()) {
                exp = reader.GetInt32(0);
                break;
            }

			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;

            return exp;
                
        }
    }
}
