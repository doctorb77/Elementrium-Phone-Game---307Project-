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
                            return false;
                        }

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
                return true;
            }

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

    }
}
