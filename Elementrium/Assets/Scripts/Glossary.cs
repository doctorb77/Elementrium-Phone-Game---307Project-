using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;
using TriumObject;
using BackpackObject;

namespace GlossaryObject
{

    public class Glossary : MonoBehaviour
    {

		private Backpack bp;            // The user's backpack

		public Glossary(Backpack bp)
		{
			this.bp = bp;
		}


		public SortedDictionary<int, int> getSortedAtomicGlossary(Backpack backpack)
		{

			// Make new ilst of int, int (atomicNumber, tableID)
			// Go in order of atomic number
			// add to hashtable
			// return atomic hashtable sorted by atomic number


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

		public SortedDictionary<string, int> getSortedCompoundGlossary(Backpack backpack)
		{

			// make new ArrayList of Strings
			// go in order of compound name
			// add to hashtable
			// return sorted by 

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
            SceneManager.LoadScene("MainGameScene");
            StateHandler.setCurrentState("MainGameScene", true, true);
        }
        void Start()
        {
            onTab = 1;
            buttonListControl.PopulateList("Atom");
        }

        public void GlossaryTabs()
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Tab_Atom")
            {
                if (onTab == 2)
                {
                    buttonListControl.PopulateList("Atom");
                    onTab = 1;
                }

            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Tab_Molecule")
            {
                if (onTab == 1)
                {
                    buttonListControl.PopulateList("Molecule");
                    onTab = 2;
                }
            }
        }
    }

}
