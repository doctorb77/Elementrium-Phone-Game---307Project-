using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Initialization;
//10-28-2017 statehandler
public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
                if (hit.transform.gameObject.name == "PlayButton") {
					if (Initialize.sh.getCurrentState().name == "MainMenu")
					{
						SceneManager.LoadScene("MainGameSpace");
						Initialize.sh.setCurrentState ("MainGameScene", true, true);
					}
                }
                else if (hit.transform.gameObject.name == "ExitButton")
				{
					if (Initialize.sh.getCurrentState().name == "Glossary"
						|| Initialize.sh.getCurrentState().name == "Achievements")
                    {
                        SceneManager.LoadScene("MainGameSpace");
						Initialize.sh.setCurrentState ("MainGameScene", true, true);
                    }
				}
			}
		}
	}
}
