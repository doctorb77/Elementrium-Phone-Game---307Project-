using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ranch;
using BudBehavior;
using UnityEngine.UI;
using StateHandling;

namespace Reaction_Text
{
    public class Reaction_Text : MonoBehaviour
    {
        public static List<string> reactants;
        public static List<string> products;
        public static List<int> reactantCount;
        public static List<int> productCount;

        public GameObject text;
        public GameObject subtext;
        private string greenS = "<color=#00ff00ff>";
        private string redS = "<color=#ff0000ff>";
        private string yellowS = "<color=#ffff00ff>";
        private string orangeS = "<color=#ffa500ff>";
        private string whiteS = "<color=#ffffffff>";
        private string colE = "</color>";
        public GameObject cr;
        // Use this for initialization
        void Start()
        {

            // Dummy values
            reactants = new List<string> { "H2", "O" };
            products = new List<string> { "H2O" };
            reactantCount = new List<int> { 1, 1 };
            productCount = new List<int> { 1 };
        }

        public static void setFormula(List<string> r, List<string> p, List<int> rC, List<int> pC)
        {
            reactants = r;
            products = p;
            reactantCount = rC;
            productCount = pC;
        }

        // Update is called once per frame
        void Update()
        {

            List<GameObject> sel = cr.GetComponent<CosmicRanch>().getSelected();

            Debug.Log(StateHandler.currentstate.name);

            if (StateHandler.currentstate.name == "Fusion")
            {
                text.GetComponent<Text>().text = "Select Two Elements to Fuse";
                if (sel.Count > 2)
                {
                    subtext.GetComponent<Text>().text = "Too Many Elements Selected";
                }
                else
                {
                    subtext.GetComponent<Text>().text = "";
                }
                return;
            }

            if (StateHandler.currentstate.name != "Group" && StateHandler.currentstate.name != "Reaction")
            {
                return;
            }

            //List<GameObject> sel = cr.GetComponent<CosmicRanch>().getSelected();
            bool allGood = true;
            string nString = "";
            int total = reactants.Count;

            for (int i = 0; i < total; i++)
            {
                int state = gotAll(reactants[i]);
                string col = "";

                if (state != 2) allGood = false;

                switch (state)
                {
                    case 0:
                        col = redS;
                        break;
                    case 1:
                        col = yellowS;
                        break;
                    case 2:
                        col = greenS;
                        break;
                    case 3:
                        col = orangeS;
                        break;
                    default:
                        col = whiteS;
                        break;
                }

                nString += col;
                if (reactantCount[i] > 1)
                    nString += reactantCount[i];
                nString += reactants[i];
                nString += colE;

                if (i != total - 1)
                    nString += " + ";
            }

            nString += " → ";

            List<string> notIn = new List<string>();

            foreach (GameObject b in sel)
            {
                if (reactants.IndexOf(b.GetComponent<BuddyBehavior>().triumformula) == -1)
                {
                    if (!notIn.Contains(b.GetComponent<BuddyBehavior>().triumformula))
                        notIn.Add(b.GetComponent<BuddyBehavior>().triumformula);
                }
            }

            if (notIn.Count != 0)
            {
                string sString = "Formula does not contain ";
                Debug.Log("TOTAL JUNK : " + notIn.Count);
                if (notIn.Count == 1)
                {
                    sString += yellowS;
                    sString += notIn[0];
                    sString += colE;
                }
                else if (notIn.Count == 2)
                {
                    sString += yellowS;
                    sString += notIn[0];
                    sString += colE;
                    sString += " or ";
                    sString += yellowS;
                    sString += notIn[1];
                    sString += colE;
                }
                else
                {
                    int size = notIn.Count;
                    for (int i = 0; i < size; i++)
                    {
                        if (i != size - 1)
                        {
                            sString += yellowS;
                            sString += notIn[i];
                            sString += colE;
                            sString += ", ";
                        }
                        else
                        {
                            sString += "or ";
                            sString += yellowS;
                            sString += notIn[i];
                            sString += colE;
                        }
                    }
                }

                subtext.GetComponent<Text>().text = sString;
                allGood = false;
            }
            else
            {
                subtext.GetComponent<Text>().text = "";
            }

            string col2 = "";
            if (allGood)
                col2 = greenS;
            else
                col2 = redS;

            int totalP = products.Count;

            for (int i = 0; i < totalP; i++)
            {
                nString += col2;
                if (productCount[i] > 1)
                    nString += productCount;
                nString += products[i];
                nString += colE;

                if (i != totalP - 1)
                    nString += " + ";
            }

            text.GetComponent<Text>().text = nString;
        }

        int gotAll(string react)
        {
            int numSet = 0;

            foreach (GameObject b in cr.GetComponent<CosmicRanch>().getSelected())
            {
                if (b.GetComponent<BuddyBehavior>().triumformula.Equals(react))
                    numSet++;
            }

            int numNeed = reactantCount[reactants.IndexOf(react)];

            if (numSet == 0)
                return 0;
            if (numSet < numNeed)
                return 1;
            if (numSet == numNeed)
                return 2;

            // numSet > numNeed
            return 3;
        }
    }
}
