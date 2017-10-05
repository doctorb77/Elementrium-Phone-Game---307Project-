using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Glossary : MonoBehaviour {

	public void ExitGlossary()
	{
		SceneManager.LoadScene("MainGameScene");
	}
}
