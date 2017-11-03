using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BackpackObject;
using TriumObject;
using Mono.Data.Sqlite;
using System.Data;
using Initialization;
using Assets.Scripts;
using Ranch;
using BudBehavior;
using System;

namespace Group {

    public class GroupHandler {

		public static GameObject ws = GameObject.Find("BuddyContainer");
		public CosmicRanch cr = Initialize.ranch;
		public Backpack bp = Initialize.player;

        public void Group(List<GameObject> selected) {

			// Make sure the list isn't empty
			if (selected.Count != 2)
			{
				return;
			}
			else if (selected[0].GetComponent<BuddyBehavior>().triumformula != selected[1].GetComponent<BuddyBehavior>().triumformula)
			{
				return;
			}

			// Obtain the database ID of the selected atoms
			int key = obtainTriumID(selected[0]);

			if (key == -1)
			{
				return;
			}

            int groupID = -1;
            string name = "none";
            string formula = "none";

            if (!canGroup(key, out groupID, out name, out formula)) {
                return;
            }

            // Make sure "out" variables are valid
            if (groupID == -1 || name == "none" || formula == "none") {
                return;
            }

            /** TODO: TEST & VERIFY **/

			/****** Update backpack and remove the selected buddies ******/
			// Destroy old Triums (x2)
			Trium old = (Trium)bp.getBP()[key];
			old.setCount(old.getCount() - 2);

			foreach (GameObject b in selected)
			{
				//Debug.Log("Removing " + b.GetComponent<BuddyBehavior>().triumformula);
				cr.GetComponent<CosmicRanch>().RemoveBuddyFromList(b);
			}


			/****** Update backpack and add new GameObject buddy ******/

			// Add new Trium to backpack
			bp.addToBackpack(groupID, name, -1);

			// Get rid of any spaces in formula
			formula = formula.Replace(" ", "");

			// Create a buddy game object from existing Prefabs
			GameObject buddy = Resources.Load("Prefabs/Triums/" + formula) as GameObject;

			// Instantiate an actual game object and transform it on the screen
			GameObject actual = GameObject.Instantiate(buddy);
			actual.transform.SetParent(ws.transform, true);
			//buddy.transform.SetParent(buttonTemplate.transform.parent, false);
			float x = (float)(UnityEngine.Random.value - 0.5) * 900;
			float y = (float)(UnityEngine.Random.value - 0.5) * 900;
			actual.transform.localPosition = new Vector3(0, -430, -1);

			// Create a new buddy object and add it to the cosmic ranch
			Buddy bud = new Buddy(0, x, y, -1, name, actual, false, false);
			cr.AddBuddyToList();

			//cr.GetComponent<CosmicRanch>().AddBuddyToList(actual); 


		}


        /**
         * 
         * Utility function to verify that a Trium (atom) can be grouped
         * 
         */
        public bool canGroup(int key, out int groupID, out string name, out string formula) {

            groupID = -1;
            name = "none";
            formula = "none";

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

            // TODO: Sprint 2/3: Check to see that they have the level capability to group

            // TODO: Sprint 2/3: Make sure we can group H2 and H2 into H3, etc

            string query = "SELECT t.ID, t.Name, t.Formula, m.GroupElementID, e.ID " +
                "FROM Trium t " +
                "INNER JOIN Molecule m ON m.ID = t.MoleculeID " +
                "INNER JOIN Element e ON e.ID = IFNULL(m.GroupElementID, -1) " +
                "WHERE e.AtomicNumber = " + t.getAtomicNumber();

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

				groupID = dbReader.GetInt32(0);
				name = dbReader.GetString(1);
                formula = dbReader.GetString(2);

				break;
			}

            // Check that the variables were updated
			if (groupID == -1 || name == "none" || formula == "none")
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
         * obtainTriumID
         * 
         * Helper method to obtain the database ID of the atom we want to fuse.
         */
		public int obtainTriumID(GameObject buddy)
		{
			string name = buddy.GetComponent<BuddyBehavior>().triumformula;

			string query = "SELECT t.ID" +
						   "FROM Trium t " +
						   "WHERE t.name = " + name;

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

    }
    
}