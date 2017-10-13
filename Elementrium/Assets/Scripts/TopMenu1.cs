using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;

public class TopMenu1 : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public bool menuIsOn;
    public bool settingsIsOn;
    public bool musicIsOn;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        menuIsOn = false;
        settingsIsOn = false;
        musicIsOn = false;
    }

    public void InteractMenu()
    {
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
    public void InteractSettings()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "SettingsButton")
        {
            if (!settingsIsOn && !menuIsOn)
            {
                anim.Play("TopSettingsDropDown");
                settingsIsOn = true;
                StateHandler.setCurrentState("Settings", true, true);
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
        else if (EventSystem.current.currentSelectedGameObject.name == "Node") 
        {
            if (settingsIsOn && !musicIsOn) {
                anim.Play("SwitchTurnOn");
                musicIsOn = true;
            } else if (settingsIsOn && musicIsOn) {
                anim.Play("SwitchTurnOff");
                musicIsOn = false;
            }
        }
    }
}
