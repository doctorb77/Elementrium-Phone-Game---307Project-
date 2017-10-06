using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;

public class StartGame : MonoBehaviour {

	public void EnterGame()
	{
        SceneManager.LoadScene("MainGameScene");
        StateHandler.setCurrentState("MainGameScene",true,true);
	}
}
