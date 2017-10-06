using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackObject;
using TriumObject;

namespace Reaction
{


    public class ReactionHandler
    {

        public void reaction(Backpack bp, int key, int numSelected)
        {

            int fusionID = -1;

            if (!canReact(bp, key, out fusionID))
            {
                return;
            }

            // Make sure groupID is not -1
            if (fusionID == -1)
            {
                return;
            }
        }

            /**
         * 
         * Utility function to verify that a Trium (atom) can be in reaction
         * 
         */
            public bool canReact(Backpack bp, int key, out int fusionID)
            {

                // Check to make sure the given key is a valid key
                if (!bp.getBP().ContainsKey(key))
                {
                    fusionID = -1;
                    return false;
                }

                // Obtain the corresponding Trium
                Trium value = (Trium)bp.getBP()[key];

                // Check that the Trium is an atom
                if (value.getAtomicNumber() == -1)
                {
                    fusionID = -1;
                    return false;
                }

                // Check to see they have at least two of the given Trium (atom)
                if (value.getCount() < 2)
                {
                    fusionID = -1;
                    return false;
                }

                // TODO: Check to see that they have the level capability to react


                // TODO: Check database for reaction
                // If there is no reaction
                // return false
                // IF there is a reaction
                // IF there is only one reaction
                // IF they have already unlocked it
                // return false
                // If they haven't unlocked it
                // return true
                // If there is more than one reaction
                // Find the first one they have not unlocked
                // If they have unlocked all
                // return false
                // If they haven't unlocked all
                // return true
                string query = "SELECT ID FROM TRIUM WHERE AtomicNumber = " + (key + 1);
                // TODO: Check database for groupings


                // SET groupID = ID
                // TODO: currently assumes that DOUBLE GROUPINGS (I.e., H2) are ID 100 + ATOMIC NUMBER
                fusionID = key + 100;

                // Check to see if they have already grouped this Trum (atom)
                if (bp.getBP().ContainsKey(fusionID))
                {
                    fusionID = -1;
                    return false;
                }


                return true;

            }

    }
}
