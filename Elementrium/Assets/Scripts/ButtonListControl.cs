using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TriumObject;
using BackpackObject;
using Initialization;


public class ButtonListControl : MonoBehaviour
{

    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private int[] intArray;

    private List<GameObject> buttons = new List<GameObject>();

    public string buttonText;

    public void PopulateList(string buttonText)
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

    public void PopulateAtomList (SortedDictionary<int, int> sd) 
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

	public void PopulateCompoundList(SortedDictionary<string, int> sd)
	{
		// <Name, Database row ID>
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
	}
}
