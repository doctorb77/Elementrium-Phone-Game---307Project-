using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;
using Initialization;

public class TopMenu1 : MonoBehaviour
{

    public static TopMenu1 Instance { get { return instance; } }
    private static TopMenu1 instance;
    public GameObject Menu;
    public Animator anim;
    public bool menuIsOn;
    public bool settingsIsOn;
	public bool musicIsOn;
    public bool soundFxIsOn;
    public bool faceIsOn;

    // Use this for initialization
    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Start()
    {
        Debug.Log("TopMenu is initialized");
        anim = Menu.GetComponent<Animator>();
        menuIsOn = false;
        settingsIsOn = false;
        musicIsOn = true;
        soundFxIsOn = true;
        faceIsOn = true;
    }

    public void InteractMenu()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "ReturnStartButton")
        {
            if (!menuIsOn && !settingsIsOn)
            {
                anim.Play("TopMenuDropDown");
                menuIsOn = true;
				Initialize.sh.setCurrentState ("Menu", true, true);
            }
            else if (menuIsOn && !settingsIsOn)
            {
                anim.Play("TopMenuRetract");
                menuIsOn = false;
				Initialize.sh.setCurrentState ("MainGameScene", true, true);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ExitMenuButton")
        {
            if (menuIsOn && !settingsIsOn)
            {
                anim.Play("TopMenuRetract");
                menuIsOn = false;
				Initialize.sh.setCurrentState ("MainGameScene", true, true);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ToStartButton")
        {
            if (menuIsOn)
            {
                SceneManager.LoadScene("StartingMenu");
				Initialize.sh.setCurrentState ("MainMenu", true, true);
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
                Initialize.sh.setCurrentState("Settings", true, true);
            }
            else if (settingsIsOn && !menuIsOn)
            {
                anim.Play("TopSettingsRetract");
                settingsIsOn = false;
				Initialize.sh.setCurrentState ("MainGameScene", true, true);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ExitSettingsButton")
        {
            if (settingsIsOn && !menuIsOn)
            {
                anim.Play("TopSettingsRetract");
                settingsIsOn = false;
				Initialize.sh.setCurrentState ("MainGameScene", true, true);
            }
        }
    }
}
