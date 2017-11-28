using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TriumObject;
using BackpackObject;
using Initialization;
using GlossaryObject;
using Mono.Data.Sqlite;
using System.Data;


public class ButtonListControl : MonoBehaviour
{

    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private int[] intArray;

    private List<GameObject> buttons = new List<GameObject>();

    public string buttonText;

	public void PopulateGlossaryList(string buttonText)
	{
		if (buttons.Count > 0)
		{

			foreach (GameObject button in buttons)
			{
				//buttons.Remove(button.gameObject);
				Destroy(button.gameObject);
			}

			buttons.Clear();
		}

		foreach (int i in intArray)
		{
			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<ButtonListButton>().SetText(buttonText + " #" + i);

			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
	}

	public void PopulateAchievementList(string buttonText)
	{
		if (buttons.Count > 0)
		{

			foreach (GameObject button in buttons)
			{
				//buttons.Remove(button.gameObject);
				Destroy(button.gameObject);
			}

			buttons.Clear();
		}

		foreach (int i in intArray)
		{
			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<ButtonListButton>().SetText(buttonText + " #" + i);

			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
	}

    public void PopulateGroupList(bool isGrouping)
	{
        RefreshButtons();

        // Populate these to be sent to the rest of the scene's needed files/scripts
        List<string> reactants;
        List<int> reactantCount;
        List<string> products;
        List<string> productCount;

        /****** Database initialization ******/
        // This is our path to the database
        string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

        // Database Connection object we will use to interact with the database
        // We can do these on the same line, they are separated to show the process
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(connectionPath);

        // This opens the connection to the database
        dbconn.Open();

        // This is a Database command object we use to run queries
        // We use the DB connection to generate this command object
        IDbCommand dbcmd = dbconn.CreateCommand();


        HashSet<int> reactions = new HashSet<int>();
        Backpack bp = Initialize.player;

        foreach (int id in bp.getBP().Keys)
        {

            string reactionP = "";

            // Run a query for ID in Trium, grab reactions
            // Store reactions into reactionP

            string isGroup = "NOT NULL";
            string isReaction = "IS NULL";

            string sqlQuery = "SELECT IFNULL(t.Reactant, 'X')"
                            + "FROM Trium t "
                            + "WHERE ID = " + id;

            // Set the next command's text to the query we made above
            dbcmd.CommandText = sqlQuery;

            // Create a reader object that executes the command
            // This will make the reader obtain a stream of information we will read
            IDataReader reader = dbcmd.ExecuteReader();

            // reader.Read() will return True or False. If true, we will execute what is in the while() loop
            while (reader.Read())
            {
                // TODO: GET stuff 
                reactionP = reader.GetString(0);

                if (reactionP == "X")
                {
                    continue;
                }

                string[] r = reactionP.Split(',');

                for (int i = 0; i < r.Length; i++)
                {
                    Debug.Log("String: " + r[i]);
                    reactions.Add(int.Parse(r[i]));
                }

            }

            reader.Close();
            reader = null;

        }

        foreach (int rID in reactions)
        {
            string reactInfo = "";
            string prodInfo = "";

            // Run query for reactionID using rID
            // Grab reactants for reactants and product for products
            string sqlQuery = "SELECT Reactants, Products "
                            + "FROM Reaction "
                            + "WHERE ID = " + rID + " AND isGrouping = " + ((isGrouping) ? 1 : 0);

            // Set the next command's text to the query we made above
            dbcmd.CommandText = sqlQuery;

            // Create a reader object that executes the command
            // This will make the reader obtain a stream of information we will read
            IDataReader reader = dbcmd.ExecuteReader();

            // reader.Read() will return True or False. If true, we will execute what is in the while() loop
            while (reader.Read())
            {
                // Obtain reactants and products
                reactInfo = reader.GetString(0);
                prodInfo = reader.GetString(1);
                break;

            }

            reader.Close();
            reader = null;

            if (reactInfo == "")
            {
                continue;
            }

            List<List<string>> data = getStrings(reactInfo, prodInfo);
            List<int> rId = new List<int>();// = data[0];
            List<int> pId = new List<int>();// = data[1];
            List<string> rFo = data[2];
            List<string> pFo = data[3];
            List<int> rC = new List<int>();
            List<int> pC = new List<int>();

            //printList("AFTERDATA PID ", data[1]);

            foreach (string count in data[0])
            {
                rId.Add(int.Parse(count));
            }

            foreach (string count in data[1])
            {
                pId.Add(int.Parse(count));
            }

            foreach (string count in data[4])
            {
                rC.Add(int.Parse(count));
            }

            foreach (string count in data[5])
            {
                pC.Add(int.Parse(count));
            }

            //   printList("rId", rId);
            //    printList("pId", pId);
            //   printList("rFo", rFo);
            //    printList("pFo", pFo);
            //  printList("rC", rC);
            //   printList("pC", pC);
            //RefreshButtons();
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<ButtonListButton>().SetText(getFormulaString(rFo, pFo, rC, pC));
            button.GetComponent<ButtonListButton>().updateReactionInfo(rFo, pFo, rC, pC);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
        }

        /*
		foreach (int i in intArray)
		{
			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<ButtonListButton>().SetText(buttonText + " #" + i);

			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
        */


        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void printList(string name, List<string> s)
    {
        for (int i = 0; i < s.Count; i++)
        {
            Debug.Log("[" + name + "] " + i + " : " + s[i]);
        }
    }

    public void printList(string name, List<int> s)
    {
        for (int i = 0; i < s.Count; i++)
        {
            Debug.Log("[" + name + "] " + i + " : " + s[i]);
        }
    }

    public string getFormulaString(List<string> r, List<string> p, List<int> rc, List<int> pc)
    {
        int total = r.Count;
        string nString = "";

        for (int i = 0; i < total; i++)
        {
            Debug.Log("i: " + i);
            if (rc[i] > 1)
                nString += rc[i];
            nString += r[i];

            if (i != total - 1)
                nString += " + ";
        }

        nString += " → ";

        int totalP = p.Count;

        for (int i = 0; i < totalP; i++)
        {
            if (pc[i] > 1)
                nString += pc[i];
            nString += p[i];

            if (i != total - 1)
                nString += " + ";
        }

        nString = nString.TrimEnd();
        if (nString.ToCharArray()[nString.Length-1] == '+')
        {
            nString = nString.Substring(0, nString.Length - 2);
        }
        return nString;
    }

    public List<List<string>> getStrings(string reactants, string products)
    {
        //   0      1      2         3      4   5
        // r(id), p(id), r(form), p(form), rc, pc
        List<List<string>> data = new List<List<string>>();

        for (int i = 0; i < 6; i++)
            data.Add(new List<string>());

        // [0] for ids, [1] for formulas
        string[] reactSep = reactants.Split('#');
        string[] prodSep = products.Split('#');

        string[] reactId = reactSep[0].Split(',');
        string[] reactFo = reactSep[1].Split(',');
        string[] prodId = prodSep[0].Split(',');
        string[] prodFo = prodSep[1].Split(',');

        for (int i = 0; i < reactId.Length; i++)
        {
            string[] triumId = reactId[i].Split('|');
            string[] triumFo = reactFo[i].Split('|');

            data[0].Add(triumId[1]);
            // Debug.Log("Add data[0] : " + triumId[1]);
            data[2].Add(triumFo[1]);
            // Debug.Log("Add data[2] : " + triumFo[1]);
            data[4].Add(triumId[0]);
            // Debug.Log("Add data[4] : " + triumId[0]);
        }

        for (int i = 0; i < prodId.Length; i++)
        {
            string[] triumId = prodId[i].Split('|');
            string[] triumFo = prodFo[i].Split('|');

            //Debug.Log("*(*(*(*(*(*(*(*(**(");
            //Debug.Log(prodId[i] + "into : " + triumId[0] + " and " + triumId[1]);

            data[1].Add(triumId[1]);
            data[3].Add(triumFo[1]);
            data[5].Add(triumId[0]);
        }

        //printList("PRODUCT ID", data[1]);

        return data;
    }



    public void GlossaryPopulateAtomList (SortedDictionary<int, int> sd) 
    {
        // <atomic Number, Database row ID>
        foreach (KeyValuePair<int, int> entry in sd)
        {
            // Key is the Database row ID of the pair
            Trium t = Initialize.player.getTrium(entry.Value);
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonListButton>().SetText(t.getName());
			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
        
    }

	public void GlossaryPopulateCompoundList(SortedDictionary<string, int> sd)
	{
        // <Name, Database row ID>
        RefreshButtons();
		foreach (int i in intArray)
		{
			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<ButtonListButton>().SetText("Molecule #" + i);

			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
        /*
		foreach (KeyValuePair<string, int> entry in sd)
		{
            // Key is the Database row ID of the pair
			Trium t = Initialize.player.getTrium(entry.Value);
			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);
			button.GetComponent<ButtonListButton>().SetText(t.getName());
			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
        */
	}
	public void PopulateList(string buttonText, int id, decimal mass, string formula, string factOne, string factTwo, string factThree)
	{
		GameObject button = Instantiate(buttonTemplate) as GameObject;
		button.SetActive(true);

		button.GetComponent<ButtonListButton>().SetTab(1);
		button.GetComponent<ButtonListButton>().SetText(buttonText);
		button.GetComponent<ButtonListButton>().SetId(id);
		button.GetComponent<ButtonListButton>().SetMass(mass);
		button.GetComponent<ButtonListButton>().SetFormula(formula);
		button.GetComponent<ButtonListButton>().SetFact1(factOne);
		button.GetComponent<ButtonListButton>().SetFact2(factTwo);
		button.GetComponent<ButtonListButton>().SetFact3(factThree);

		button.transform.SetParent(buttonTemplate.transform.parent, false);
		buttons.Add(button);
	}
	public void PopulateSecondList(string buttonText, string commonname, decimal mass, string formula, string factOne, string factTwo, string factThree)
	{
		GameObject button = Instantiate(buttonTemplate) as GameObject;
		button.SetActive(true);

		button.GetComponent<ButtonListButton>().SetTab(2);
		button.GetComponent<ButtonListButton>().SetText(buttonText);
		button.GetComponent<ButtonListButton>().SetCommon(commonname);
		button.GetComponent<ButtonListButton>().SetMass(mass);
		button.GetComponent<ButtonListButton>().SetFormula(formula);
		button.GetComponent<ButtonListButton>().SetFact1(factOne);
		button.GetComponent<ButtonListButton>().SetFact2(factTwo);
		button.GetComponent<ButtonListButton>().SetFact3(factThree);

		button.transform.SetParent(buttonTemplate.transform.parent, false);
		buttons.Add(button);
	}
	public void RefreshButtons()
	{
		if (buttons.Count > 0)
		{
			foreach (GameObject button in buttons)
			{
				Destroy(button.gameObject);
			}
			buttons.Clear();
		}
	}
}
