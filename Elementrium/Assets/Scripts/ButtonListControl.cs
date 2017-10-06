using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
