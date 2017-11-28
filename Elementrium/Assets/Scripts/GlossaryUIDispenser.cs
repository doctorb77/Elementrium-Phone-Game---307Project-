using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Initialization;

public class GlossaryUIDispenser : MonoBehaviour {
	public static GlossaryUIDispenser Instance { get { return instance; } }
	private static GlossaryUIDispenser instance;
	public static List<Sprite> sprites = new List<Sprite>();

	public Sprite sprH;
	public Sprite sprHe;

	void Awake()
	{
		if (Instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		sprites.Add (sprH);
		sprites.Add (sprHe);
		//Debug.Log ("GlossaryUIDispenser");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
