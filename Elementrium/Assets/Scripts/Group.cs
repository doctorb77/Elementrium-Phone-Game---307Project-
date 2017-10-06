using UnityEngine;
using System.Collections;
using BackpackObject;
using TriumObject;

namespace Group {

    public class GroupHandler {

        public void group(Backpack bp, int key, int numSelected) {

            int groupID = -1;

            if (!canGroup(bp, key, out groupID)) {
                return;
            }

            // Make sure groupID is not -1
            if (groupID == -1) {
                return;
            }


            // TODO: Obtain infomation from database
            string query = "SELECT NAME FROM TRIUM WHERE ID = " + groupID;

            // TODO: Assumes Grouping 2 hydrogen atoms
            // TODO: MAKE DYNAMIC

            string name = "H2";
            int atomicNumber = -1;

            // Add the new grouping to backpack
            bp.addToBackpack(groupID, name, atomicNumber);

        }


        /**
         * 
         * Utility function to verify that a Trium (atom) can be grouped
         * 
         */
        public bool canGroup(Backpack bp, int key, out int groupID) {
            
			// Check to make sure the given key is a valid key
			if (!bp.getBP().ContainsKey(key))
			{
				groupID = -1;
				return false;
			}

			// Obtain the corresponding Trium
			Trium value = (Trium)bp.getBP()[key];

			// Check that the Trium is an atom
			if (value.getAtomicNumber() == -1)
			{
				groupID = -1;
				return false;
			}

			// Check to see they have at least two of the given Trium (atom)
			if (value.getCount() < 2)
			{
				groupID = -1;
				return false;
			}

			// TODO: Check to see that they have the level capability to group


            // TODO: Check database for groupings
                // If there is no grouping
                    // return false
                // IF there is a grouping
                    // IF there is only one grouping
                        // IF they have already unlocked it
                            // return false
                        // If they haven't unlocked it
                            // return true
                    // If there is more than one grouping
                        // Find the first one they have not unlocked
                            // If they have unlocked all
                                // return false
                            // If they haven't unlocked all
                                // reurn true
			string query = "SELECT ID FROM TRIUM WHERE AtomicNumber = " + (key + 1);
			// TODO: Check database for groupings


			// SET groupID = ID
			// TODO: currently assumes that DOUBLE GROUPINGS (I.e., H2) are ID 100 + ATOMIC NUMBER
			groupID = key + 100;

			// Check to see if they have already grouped this Trum (atom)
			if (bp.getBP().ContainsKey(groupID))
			{
				groupID = -1;
				return false;
			}


			return true;

        }

    }
    
}