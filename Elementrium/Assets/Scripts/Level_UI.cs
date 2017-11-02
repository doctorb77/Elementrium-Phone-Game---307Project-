using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackpackObject;
using UnityEngine.UI;

public class Level_UI : MonoBehaviour {

    public GameObject text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.GetComponent<Text>().text = (""+Backpack.level);
	}
}
