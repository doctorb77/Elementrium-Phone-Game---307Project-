using UnityEngine;
using System.Collections;
using BackpackObject;
using TriumObject;

using Mono.Data.Sqlite;
using System.Data;

namespace Fusion {

    public class FusionHandler {
        

        /**
         * fuse()
         * 
         * Algorithm used to "fuse" atoms within a User's backpack. 
         * 
         */
        public void fuse(Backpack bp, int key) {

            /* TODO: Support selecting multiple atoms at a time
            if (keyOne != keyTwo) {
                return;
            }
            */

            int fusionID = -1;
            string atomName = "none";
            int atomicNumber = -1;

            // Make sure we are able to fuse, 
            // If we are, obtain relavant information and store in "out" variables
            if (!canFuse(bp, key, out fusionID, out atomName, out atomicNumber)) {
                return;
            }

            // Make sure "out" variables are now valid
            if (fusionID == -1 || atomName == "none" || atomicNumber == -1) {
                return;
            }

            // Destroy old Trium's
            Trium old = (Trium) bp.getBP()[key];
            old.setCount(old.getCount() - 2);

			// TODO: CODE TO REMOVE BUDDIES!
			    // Remove them from BuddyList
			    // Remove them from CosmicRanch



			// Add new Trium to Ranch
			bp.addToBackpack(fusionID, atomName, atomicNumber);

            // TODO: CREATE BUDDY OBJECT AND PLACE IN COSMIC RANCH!
            // Add to global buddy list
            // Transpose on CosmicRanch

        }


        /**
         * canFuse()
         * 
         * Utility method to ensure that the User can indeed fuse two of the same atom.
         * 
         * Updates output variables upon valid Fusion attempts. 
         *
         */
        public bool canFuse(Backpack bp, int key, out int fusionID, out string name, out int AtomicNumber) {

            // Set initial value of "out" variables
			fusionID = -1;
			name = "none";
			AtomicNumber = -1;

            // Check to make sure the given key is a valid key
            if (!bp.getBP().ContainsKey(key)) {
                return false;
            }

            // Obtain the corresponding Trium
            Trium t = (Trium) bp.getBP()[key];

            // Check that the Trium is an atom
            if (t.getAtomicNumber() == -1) {
                return false;
            }

			// Check to see they have at least two of the given Trium (atom)
			if (t.getCount() < 2) {
                return false;
            }

            int oldElementID = t.getAtomicNumber();


			// TODO: Check to see that they have the level capability to fuse


            // Set up string to Query from database
			string query = "SELECT t.ID, t.Name, e.AtomicNumber " +
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

            while (dbReader.Read()) {
                
                fusionID = dbReader.GetInt32(0);
                name = dbReader.GetString(1);
                AtomicNumber = dbReader.GetInt32(2);

                break;
            }

            if (fusionID == -1 || name == "none" || AtomicNumber == -1) {
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
