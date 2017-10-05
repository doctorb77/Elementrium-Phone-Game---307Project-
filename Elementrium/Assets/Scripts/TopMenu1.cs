using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TopMenu1 : MonoBehaviour {

	public GameObject Menu;
	public Animator anim;
	public bool menuIsOn;
    public bool settingsIsOn;

    //public bool[] colorArray = { true, false, false, false, false };
    //black, red, purple, blue, green
    //public static int colorChoice;
    //public static Color color;

	// Use this for initialization
	void Start () {
		anim = Menu.GetComponent<Animator>();
		menuIsOn = false;
        settingsIsOn = false;
	}
	
    public void InteractMenu () {
        if (EventSystem.current.currentSelectedGameObject.name == "ReturnStartButton")
        {
			if (!menuIsOn && !settingsIsOn)
			{
				anim.Play("TopMenuDropDown");
				menuIsOn = true;
			}
			else if (menuIsOn && !settingsIsOn)
			{
				anim.Play("TopMenuRetract");
				menuIsOn = false;
			}
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ExitMenuButton")
        {
			if (menuIsOn && !settingsIsOn)
			{
				anim.Play("TopMenuRetract");
				menuIsOn = false;
			}
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ToStartButton") 
        {
			if (menuIsOn)
			{
				SceneManager.LoadScene("StartingMenu");
			}
        }
	}
    public void InteractSettings () {
        if (EventSystem.current.currentSelectedGameObject.name == "SettingsButton")
        {
            if (!settingsIsOn && !menuIsOn)
            {
                anim.Play("TopSettingsDropDown");
                settingsIsOn = true;
            }
            else if (settingsIsOn && !menuIsOn)
            {
                anim.Play("TopSettingsRetract");
                settingsIsOn = false;
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ExitSettingsButton")
        {
			if (settingsIsOn && !menuIsOn)
			{
				anim.Play("TopSettingsRetract");
				settingsIsOn = false;
			}
        } 
        /*
        else if (EventSystem.current.currentSelectedGameObject.name == "Black")
        {
            colorChoice = 1;
            color = Color.black;
            //CosmicRanch.background.material.SetColor("_Color", color);
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "Red")
		{
			colorChoice = 2;
			color = new Color(58, 0, 0);
            //CosmicRanch.background.material.SetColor("_Color", color);
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "Purple")
		{
			colorChoice = 3;
			color = new Color(38, 0, 58);
            //CosmicRanch.background.material.SetColor("_Color", color);
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "Blue")
		{
			colorChoice = 4;
			color = new Color(0, 0, 58);
            //CosmicRanch.background.material.SetColor("_Color", color);
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "Green")
		{
			colorChoice = 5;
			color = new Color(0, 30, 3);
            //CosmicRanch.background.material.SetColor("_Color", color);
		}
		*/
    }
}
