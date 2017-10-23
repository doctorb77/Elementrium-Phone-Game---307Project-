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

		public static GameObject ws = GameObject.Find("BuddyContainer");
		public CosmicRanch cr = Initialize.ranch;
		public Backpack bp = Initialize.player;

		/**
         * fuse()
         * 
         * Algorithm used to "fuse" atoms within a User's backpack. 
         * 
         */
		public void fuse(List<GameObject> selected)
		{

			/* TODO: Support selecting multiple atoms at a time
            if (keyOne != keyTwo) {
                return;
            }
            */

			// Make sure the list isn't empty
			if (selected.Count != 2)
			{
				return;
            } else if (selected[0].GetComponent<BuddyBehavior>().triumformula != selected[1].GetComponent<BuddyBehavior>().triumformula) {
                return;
            }

            // Obtain the database ID of the selected atoms
            int key = obtainAtomID(selected[0]);

            if (key == -1) {
                return;
            }

			int fusionID = -1;  // The database ID of the trium
			string atomName = "none";  // The name of the trium
			string formula = "none";  // The Formula of the trium
			int atomicNumber = -1;  // The Atomic Number of the trium

			// Make sure we are able to fuse, 
			// If we are, obtain relavant information and store in "out" variables
			if (!canFuse(key, out fusionID, out atomName, out formula, out atomicNumber))
			{
				return;
			}

			// Make sure "out" variables are now valid
			if (fusionID == -1 || atomName == "none" || formula == "none" || atomicNumber == -1)
			{
				return;
			}

			/****** Update backpack and remove the selected buddies ******/
			// Destroy old Triums (x2)
			Trium old = (Trium)bp.getBP()[key];
			old.setCount(old.getCount() - 2);

			foreach (GameObject b in selected)
			{
				Debug.Log("Removing " + b.GetComponent<BuddyBehavior>().triumformula);
				cr.GetComponent<CosmicRanch>().RemoveBuddyFromList(b);
			}


			/****** Update backpack and add new GameObject buddy ******/

			// Add new Trium to backpack
			bp.addToBackpack(fusionID, atomName, atomicNumber);

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
			Buddy bud = new Buddy(0, x, y, atomicNumber, atomName, actual, false, false);
			cr.AddBuddyToList();

			//cr.GetComponent<CosmicRanch>().AddBuddyToList(actual); 


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
			string query = "SELECT t.ID, t.Name, t.Formula, e.AtomicNumber " +
						   "FROM Trium t " +
						   "INNER JOIN Element e ON IFNULL(t.ElementID, -1) = e.ID " +
						   "WHERE e.AtomicNumber = " + (oldElementID + 1);

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
