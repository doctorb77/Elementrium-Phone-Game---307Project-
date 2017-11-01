using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TriumObject;
using BackpackObject;
using Initialization;
using GlossaryObject;


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

    public void PopulateGroupList(string buttonText)
	{
        RefreshButtons();
		foreach (int i in intArray)
		{
			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<ButtonListButton>().SetText(buttonText + " #" + i);

			button.transform.SetParent(buttonTemplate.transform.parent, false);
			buttons.Add(button);
		}
	}

    public void PopulateReactionList(string buttonText)
    {
        RefreshButtons();
        foreach (int i in intArray)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            if (i == 1)
            {
                // TEMP // - Will n
                button.GetComponent<ButtonListButton>().SetText("H2O");
                button.GetComponent<ButtonListButton>().field = "Reaction";
            }
            else
                button.GetComponent<ButtonListButton>().SetText(buttonText + " #" + i);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
        }
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
	public void PopulateList(string buttonText, int id, decimal mass, string formula)//,string factOne, string factTwo, string factThree
	{
		GameObject button = Instantiate(buttonTemplate) as GameObject;
		button.SetActive(true);

		button.GetComponent<ButtonListButton>().SetText(buttonText);
		button.GetComponent<ButtonListButton>().SetId(id);
		button.GetComponent<ButtonListButton>().SetMass(mass);
		button.GetComponent<ButtonListButton>().SetFormula(formula);
		//button.GetComponent<ButtonListButton>().SetText(buttonText);
		//button.GetComponent<ButtonListButton>().SetText(buttonText);
		//button.GetComponent<ButtonListButton>().SetText(buttonText);

		//Camera.main.GetComponent<Glossary> ().setnames (buttonText);
		//button.GetComponent<ButtonListButton>().settingname(buttonText);
		//button.GetComponentInParent<Glossary> ().setnames (buttonText);
		//button.GetComponent<Glossary> ().setnames (buttonText);//null ref exception
		//button.GetComponent<Glossary> ().setinfo (id, mass);
		//button.GetComponent<Glossary> ().setfact1 (factOne);
		//button.GetComponent<Glossary> ().setfact2 (factTwo);
		//button.GetComponent<Glossary> ().setfact3 (factThree);

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
