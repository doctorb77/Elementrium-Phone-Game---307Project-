using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenu1 : MonoBehaviour {

	public GameObject Menu;
	public Animator anim;
	public bool menuIsOn;
    public bool settingsIsOn;

	// Use this for initialization
	void Start () {
		anim = Menu.GetComponent<Animator>();
		menuIsOn = false;
        settingsIsOn = false;
	}
	
    public void InteractMenu () {
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
    public void InteractSettings () {
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
    public void ReturnToStart () {
		if (menuIsOn)
		{
			SceneManager.LoadScene("StartingMenu");
		}
    }
}
