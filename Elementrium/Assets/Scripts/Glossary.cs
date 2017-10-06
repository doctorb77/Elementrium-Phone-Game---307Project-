using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;

public class Glossary : MonoBehaviour {

	public ButtonListControl buttonListControl;
    public int onTab;

	public void ExitGlossary()
	{
		SceneManager.LoadScene("MainGameScene");
        StateHandler.setCurrentState("MainGameScene",true,true);
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
