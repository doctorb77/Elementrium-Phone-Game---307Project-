using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenu : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.gameObject.name == "ReturnStartButton")
				{
                    if (!menuIsOn && !settingsIsOn)
					{
						anim.Play("TopMenuDropDown");
						menuIsOn = true;
					}
					
				}
				else if (hit.transform.gameObject.name == "ExitMenuButton")
				{
					if (menuIsOn)
					{
						anim.Play("TopMenuRetract");
                        menuIsOn = false;
					}

				}
				else if (hit.transform.gameObject.name == "ToStartButton")
				{
					if (menuIsOn)
					{
						SceneManager.LoadScene("StartingMenu");
					}

				}
				else if (hit.transform.gameObject.name == "SettingsButton")
				{
                    if (!settingsIsOn && !menuIsOn)
					{
						anim.Play("TopSettingsDropDown");
						settingsIsOn = true;
					}

				}
				else if (hit.transform.gameObject.name == "ExitSettingsButton")
				{
					if (settingsIsOn)
					{
						anim.Play("TopSettingsRetract");
						settingsIsOn = false;
					}

				}
			}
		}
	}
}
