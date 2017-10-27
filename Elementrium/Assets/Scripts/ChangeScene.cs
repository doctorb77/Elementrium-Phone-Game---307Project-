using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Initialization;

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
					if (SceneManager.GetActiveScene().name == "StartingMenu")
					{
						SceneManager.LoadScene("MainGameSpace");
						Initialize.sh.setCurrentState ("MainGameScene", true, true);
					}
                }
                else if (hit.transform.gameObject.name == "ExitButton")
				{
                    if (SceneManager.GetActiveScene().name == "Glossary"
                       || SceneManager.GetActiveScene().name == "Achievements")
                    {
                        SceneManager.LoadScene("MainGameSpace");
						Initialize.sh.setCurrentState ("MainGameScene", true, true);
                    }
				}
			}
		}
	}
}
