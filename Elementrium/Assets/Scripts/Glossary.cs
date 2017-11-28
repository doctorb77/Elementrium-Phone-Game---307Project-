using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;
using TriumObject;
using BackpackObject;
using Initialization;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using Assets.Scripts;
using System;

namespace GlossaryObject
{

    public class Glossary: MonoBehaviour
    {
		
        public GameObject glossary;
		public static bool displayOpen = false;
		public Animator GlossaryAnim;
		public Backpack bp = Initialize.player;
		//[SerializeField]
		public Text names;
		public Text info;
		public Text fact1;
		public Text fact2;
		public Text fact3;
		public Image myimg;

		public void setnames(string name) {
			this.names.text = name.ToString ();
		}
		public void setinfo(string id, string mass, string formula) {
			this.info.text = "Symbol: " + formula + "\n Atomic Number: " + id + "\n Atomic Mass: " + mass;
		}
		public void setinfo2(string commonname, string mass, string formula) {
			this.info.text = "Symbol: " + formula + "\n";
			if (commonname != ""){
				this.info.text = this.info.text+"Common Name: " + commonname + "\n";
			}
			this.info.text = this.info.text+"Atomic Mass: " + mass;
		}
		public void setfact1(string newfact1) {
			this.fact1.text = newfact1.ToString ();
		}
		public void setfact2(string newfact2) {
			this.fact2.text = newfact2.ToString ();
		}
		public void setfact3(string newfact3) {
			this.fact3.text = newfact3.ToString ();
		}
		public void setimg (Sprite spr2) {
			this.myimg = GetComponent<Image> ();
			this.myimg.sprite = spr2;
		}


		public SortedDictionary<int, int> getSortedAtomicGlossary()
		{

            Backpack backpack = Initialize.player;


			SortedDictionary<int, int> sd = new SortedDictionary<int, int>();

			Hashtable table = backpack.getBP();


			foreach (int key in table)
			{
				Trium t = (Trium)table[key];

				if (t.getAtomicNumber() == -1)
				{
					continue;
				}

				sd.Add(t.getAtomicNumber(), key);

			}

			return sd;
		}

		public SortedDictionary<string, int> getSortedCompoundGlossary()
		{


            Backpack backpack = Initialize.player;

			SortedDictionary<string, int> sd = new SortedDictionary<string, int>();

			Hashtable table = backpack.getBP();


			foreach (int key in table)
			{
				Trium t = (Trium)table[key];

				if (t.getAtomicNumber() != -1)
				{
					continue;
				}

				sd.Add(t.getName(), key);

			}

			return sd;

		}

        public ButtonListControl buttonListControl;
        public int onTab;

        public void ExitGlossary()
        {
            //SceneManager.LoadScene("MainGameScene");
            glossary.SetActive(false);
            Initialize.sh.setCurrentState("MainGameScene", true, true);
        }
        void Start()
        {
            onTab = 1;
            //buttonListControl.PopulateGlossaryList("Atom");
            //print(Initialize.buddyList.Count);
			PopulateGlossaryAtoms();
        }

        public void GlossaryTabs()
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Tab_Atom")
            {
                if (onTab == 2)
                {
                    PopulateGlossaryAtoms();
                    onTab = 1;
                }

            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Tab_Molecule")
            {
                if (onTab == 1)
                {
                    //buttonListControl.GlossaryPopulateCompoundList(getSortedCompoundGlossary());
                    PopulateGlossaryMolecules();
					onTab = 2;
                }
            }
        }
		public void CloseTab()
		{
			GlossaryAnim.Play("GlossaryInfoRetract");
			displayOpen = false;
		}
		public void PopulateGlossaryAtoms() {
            buttonListControl.RefreshButtons();
			
			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";
		
			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);

			dbconn.Open();

			IDbCommand dbcmd = dbconn.CreateCommand();
			//string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.ElementID, Element.AtomicNumber, Trium.Formula, " +
				//"Trium.Mass, Trium.FactOne, Trium.FactTwo, Trium.FactThree FROM Trium"
				//+ "INNER JOIN Element ON Trium.ElementID=Element.ID";                 
			string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.Formula, Trium.Mass, IFNULL(Trium.FactOne, 'X'), IFNULL(Trium.FactTwo, 'X'), IFNULL(Trium.FactThree, 'X'), Element.AtomicNumber " + 
				"FROM Trium INNER JOIN Element ON Element.ID = IFNULL(Trium.ElementID, -1) WHERE IFNULL(Element.AtomicNumber, -1) <= 92";
			dbcmd.CommandText = sqlQuery;

			IDataReader reader = dbcmd.ExecuteReader();
			// reader.Read() will return True or False. If true, we will execute what is in the while() loop
			while (reader.Read())
			{
				
				Initialize.sh.setCurrentState("Glossary", true, true);
				int id = reader.GetInt32(0);            /* the ID column of the Trium */
				string element = reader.GetString(1);      /* the Name column of the Trium */
				string formula = reader.GetString(2);    /* the Formula column of the Trium */
				decimal mass = reader.GetDecimal (3);
				string first = reader.GetString (4);
				if (first == "X") {
					first = "";
				}
				string second = reader.GetString (5);
				if (second == "X") {
					second = "";
				}
				string third = reader.GetString (6);
				if (third == "X") {
					third = "";
				}
				int atomicnum = reader.GetInt32(7);
				//Sprite spr;
			    //Debug.Log ("Image: "+myimg.sprite.name);
				//if (GlossaryUIDispenser.Instance.sprH.name.Contains (formula)) {
				//Debug.Log ("#" +(atomicnum - 1));

				if ((atomicnum - 1) < GlossaryUIDispenser.sprites.Count) {
					if (GlossaryUIDispenser.sprites[atomicnum - 1] != null) {
						Sprite spr = GlossaryUIDispenser.sprites[atomicnum - 1];
						if (bp.getTrium(id) != null) {
							buttonListControl.PopulateList (element, atomicnum, mass, formula, first, second, third, spr);
						}
					}
				}
				/*if (bp.getTrium(id) != null) {
					buttonListControl.PopulateList (element, atomicnum, mass, formula, first, second, third, spr);
				}*/
				/*this.names.text = element.ToString();
				this.info.text = "Atomic Number: " +id;*/
			}


			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;
		}
		public void PopulateGlossaryMolecules() {
			buttonListControl.RefreshButtons();

			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";

			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);

			dbconn.Open();

			IDbCommand dbcmd = dbconn.CreateCommand();
			//string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.ElementID, Element.AtomicNumber, Trium.Formula, " +
			//"Trium.Mass, Trium.FactOne, Trium.FactTwo, Trium.FactThree FROM Trium"
			//+ "INNER JOIN Element ON Trium.ElementID=Element.ID";                 
			string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.Formula, Trium.Mass, IFNULL(Trium.FactOne, 'X'), IFNULL(Trium.FactTwo, 'X'), IFNULL(Trium.FactThree, 'X'), IFNULL(Molecule.CommonName, 'X') " + 
				"FROM Trium INNER JOIN Molecule on Molecule.ID = IFNULL(Trium.MoleculeID, -1) WHERE Trium.ID > 92 ORDER BY Trium.Name ASC";
			dbcmd.CommandText = sqlQuery;

			IDataReader reader = dbcmd.ExecuteReader();
			// reader.Read() will return True or False. If true, we will execute what is in the while() loop
			while (reader.Read())
			{
				

				int id = reader.GetInt32(0);            /* the ID column of the Trium */
				string molecule = reader.GetString(1);      /* the Name column of the Trium */
				string formula = reader.GetString(2);    /* the Formula column of the Trium */
				decimal mass = reader.GetDecimal (3);
				string commonname = reader.GetString(7);
				if (commonname == "X") {
					commonname = "";
				}
				string first = reader.GetString (4);
				if (first == "X") {
					first = "";
				}
				string second = reader.GetString (5);
				if (second == "X") {
					second = "";
				}
				string third = reader.GetString (6);
				if (third == "X") {
					third = "";
				}
				Sprite spr = null;
				if (GlossaryUIDispenser.Instance.sprH.name.Contains (formula)) {
					spr = GlossaryUIDispenser.Instance.sprH;
				}
				if (bp.getTrium(id) != null) {
					buttonListControl.PopulateSecondList (formula, commonname, mass, formula, first, second, third, spr);
				}
				/*this.names.text = element.ToString();
				this.info.text = "Atomic Number: " +id;*/
			}


			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;
		}
		
    }

}
