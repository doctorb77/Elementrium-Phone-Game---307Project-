using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackObject;
using TriumObject;
using Ranch;
using BudBehavior;

namespace Reaction
{

    public class ReactionHandler
    {
        public static GameObject ws = GameObject.Find("BuddyContainer");

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

        public static bool reactCurrent(List<GameObject> selected, List<string> reactant2, List<int> reactantCount2, List<string> product2, List<int> productCount2)
        {
            //Debug.Log("HERE in React");
            // Check if requirements are met
            bool req = false;

            if (selected.Count == 0)
                return false;

            List<string> reactant = new List<string>(reactant2.ToArray());
            List<int> reactantCount = new List<int>(reactantCount2.ToArray());
            List<string> product = new List<string>(product2.ToArray());
            List<int> productCount = new List<int>(productCount2.ToArray());

            for (int i = 0; i < 2; i++)
            {
                if (selected.Count == 2)
                { /*
                    Debug.Log("Selected \"" + selected[i].GetComponent<BuddyBehavior>().triumformula + "\"   Reactant : \"" + reactant[i] + "\"");
                    Debug.Log(reactant[i].Equals(selected[i].GetComponent<BuddyBehavior>().triumformula)); */
                }
            }

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
                        return false;
                    }
                }
                else // Something selected that shouldn't be
                    return false;
            }

            req = true;

            foreach (int total in reactantCount)
            {
                if (total != 0)
                {
                    req = false;
                    return false;
                }
            }

            if (req)
            {
                // TESTING FOR EXP BAR : REMOVE LATER
                Backpack.gainExp(170);

                GameObject cr = GameObject.FindGameObjectWithTag("CosmicRanch");//.GetComponent<CosmicRanch>().AddBuddyToList();

                foreach (GameObject buddy in selected)
                {
                    Debug.Log("Removing " + buddy.GetComponent<BuddyBehavior>().triumformula);
                    cr.GetComponent<CosmicRanch>().RemoveBuddyFromList(buddy);
                }

                foreach (string nTrium in product)
                {
                    for (int i = 0; i < productCount[product.IndexOf(nTrium)]; i++)
                    {
                        Debug.Log("nTrium : " + nTrium + " Path : \'\"Prefabs/Triums/" + nTrium+"\"\'");
                        GameObject buddy = Resources.Load("Prefabs/Triums/"+nTrium) as GameObject;
                        GameObject actual = GameObject.Instantiate(buddy);

                        actual.transform.SetParent(ws.transform, true);
                        //buddy.transform.SetParent(buttonTemplate.transform.parent, false);

                        float x = (float)(UnityEngine.Random.value - 0.5) * 900;
                        float y = (float)(UnityEngine.Random.value - 0.5) * 900;

                        actual.transform.localPosition = new Vector3(0, 0, -1);
                        cr.GetComponent<CosmicRanch>().AddBuddyToList();
                    }
                }

                return true;
            }

            return false;
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
