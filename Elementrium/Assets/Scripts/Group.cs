using UnityEngine;
using System.Collections;
using BackpackObject;
using TriumObject;
using Mono.Data.Sqlite;
using System.Data;

namespace Group {

    public class GroupHandler {

        public void Group(Backpack bp, int key) {

            /*
                // Handle numSelected
                // TODO: Sprint 2/3

             */

            int groupID = -1;
            string name = "none";
            string formula = "none";

            if (!canGroup(bp, key, out groupID, out name, out formula)) {
                return;
            }

            // Make sure "out" variables are valid
            if (groupID == -1 || name == "none" || formula == "none") {
                return;
            }


            // Decrease count of grouped Trium
            Trium old = bp.getTrium(key);
            old.setCount(old.getCount() - 2);

            // TODO: Remove 2 buddies from list
            // TODO: Destroy buddy objects from CosmicRanch


            bp.addToBackpack(groupID, name, -1);
			// TODO: Create new buddy object
            // Transpose on CosmicRanch


		}


        /**
         * 
         * Utility function to verify that a Trium (atom) can be grouped
         * 
         */
        public bool canGroup(Backpack bp, int key, out int groupID, out string name, out string formula) {

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

    }
    
}