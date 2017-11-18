﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BudBehavior;
using Ranch;

public class ZoomInfo : MonoBehaviour {

    public static ZoomInfo Instance { get { return instance; } }
    private static ZoomInfo instance;
    public bool infoOn;
    public bool deleteOn;
    public Animator ZoomInfoDisplay;
    public Animator DeleteAnim;
    public GameObject targetBuddy;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public Text nameDisplay;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
        infoOn = false;
        deleteOn = false;
        level1.SetActive(true);
        level2.SetActive(false);
        level3.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenZoomin(GameObject buddy) {
        if (!infoOn)
        {
            infoOn = true;
            targetBuddy = buddy;
            nameDisplay.text = buddy.GetComponent<BuddyBehavior>().triumName;
            ZoomInfoDisplay.Play("ZoomInfoPopIn");
        }
    }
    public void CloseZoomin() {
        if (infoOn) {
            infoOn = false;
            ZoomInfoDisplay.Play("ZoomInfoPopOut");
        }
    }
    public void OpenDelete() {
        if (!deleteOn) {
            deleteOn = true;
            DeleteAnim.Play("DeletePopIn");
        }
    }
    public void CloseDelete() {
        if (deleteOn) {
            deleteOn = false;
            DeleteAnim.Play("DeletePopOut");
        }
    }
    public void ConfirmDelete() {
        if (!deleteOn) {
            if (targetBuddy != null) {
                CosmicRanch.Instance.RemoveBuddyFromList(targetBuddy);
            }
        }
        CloseDelete();
    }

}