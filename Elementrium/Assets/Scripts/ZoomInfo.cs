using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BudBehavior;
using Ranch;
using Initialization;

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
        int triumID = 1;
        if (targetBuddy != null)
        {
            triumID = targetBuddy.GetComponent<BuddyBehavior>().TriumID;
            //Debug.Log("triumID: " + triumID);
            int tier = Initialize.player.getTrium(triumID).getTier();

            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);

            if (tier >= 2)
                level2.SetActive(true);
            if (tier >= 3)
                level3.SetActive(true);
        }
    }
    public void OpenZoomin() {
        if (Initialize.sh.getCurrentState().name == "MainGameScene")
        {
            if (!infoOn)
            {
                infoOn = true;
                targetBuddy = GameObject.FindWithTag("ZoomedBuddy");
                nameDisplay.text = targetBuddy.GetComponent<BuddyBehavior>().triumName;
				Initialize.sh.setCurrentState ("ZoomIn", true, true);
                if (targetBuddy != null)
                    ZoomInfoDisplay.Play("ZoomInfoPopIn");
            }
        }
    }
    public void CloseZoomin() {
        if (infoOn) {
            infoOn = false;
            targetBuddy = GameObject.FindWithTag("ZoomedBuddy");
            targetBuddy.gameObject.tag = "Buddy";
			if (Initialize.sh.getCurrentState ().name == "ZoomIn") {
				Initialize.sh.setCurrentState ("MainGameScene", true, true);
			}
            ZoomInfoDisplay.Play("ZoomInfoPopOut");
        }
    }
    public void OpenDelete() {
        if (!deleteOn) {
            deleteOn = true;
            targetBuddy = GameObject.FindWithTag("ZoomedBuddy");
            if (targetBuddy != null)
                DeleteAnim.Play("DeletePopIn");
        }
    }
    public void CancelDelete() {
        if (deleteOn) {
			deleteOn = false;
			DeleteAnim.Play("DeletePopOut");
        }
    }
    public void ConfirmDelete() {
        if (deleteOn) {
            deleteOn = false;
			Initialize.sh.setCurrentState ("MainGameScene", true, true);
            targetBuddy = GameObject.FindWithTag("ZoomedBuddy");
            targetBuddy.gameObject.tag = "Buddy";
            if (targetBuddy != null) {
                CosmicRanch.Instance.RemoveBuddyFromList(targetBuddy);
            }
			DeleteAnim.Play("DeletePopOut");
			infoOn = false;
			ZoomInfoDisplay.Play("ZoomInfoPopOut");
        }
    }
}
