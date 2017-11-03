﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StateHandling;
using GlossaryObject;
using Ranch;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Initialization;

//editted to add Statehandler on 10/28/2017
public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    private Text myText;

	private string myId;
	private int tab;
	private string myMass;
	private string myCommonName;
	private string myFormula;
	private string myFact1;
	private string myFact2;
	private string myFact3;
    public string field; // Group, Reaction, None
    public Animator GlossaryAnim;
    public Animator RightMenuAnim;
    public Animator ScrolllistAnim;
    public Animator SelectorAnim;
    public CosmicRanch ranch;

	public void SetTab(int num)
	{
		tab = num;
	}
    public void SetText(string textString)
    {
        myText.text = textString;
    }
	public void SetId(int id)
	{
		myId = ""+id;
	}
	public void SetMass(decimal mass)
	{
		myMass = "" + mass;
	}
	public void SetFormula(string formula)
	{
		myFormula = formula;
	}
	public void SetFact1(string textString)
	{
		myFact1 = textString;
	}
	public void SetFact2(string textString)
	{
		myFact2 = textString;
	}
	public void SetFact3(string textString)
	{
		myFact3 = textString;
	}
	public void SetCommon(string commonname) {
		myCommonName = commonname;
	}
    public void OnClick()
    {
        //Debug.Log("TESTWETF");
		if (Initialize.sh.getCurrentState().name == "MainGameScene")
        {
            //Debug.Log("BUTTON CLICKED");
            GameObject rightMenu = GameObject.Find("RightActivation");
            rightMenu.GetComponent<RightMenu1>().Play();
        }
		else if (Initialize.sh.getCurrentState().name == "Glossary")
        {
            if (!Glossary.displayOpen)
            {
				Camera.main.GetComponent<Glossary> ().setnames (myText.text);
				if (tab == 1) {//atom
					Camera.main.GetComponent<Glossary> ().setinfo (myId, myMass, myFormula);
				} else if (tab == 2) {//molecule
					Camera.main.GetComponent<Glossary> ().setinfo2 (myCommonName, myMass, myFormula);
				}
				//Camera.main.GetComponent<Glossary> ().setnames (myText.text);
                GlossaryAnim.Play("GlossaryInfoDisplayPopUp");
				//Debug.Log ("Played popup");
				//Glossary.popupInfo (EventSystem.current.currentSelectedGameObject.name);


            }
        }


    }
    public void StartSelector()
    {
        if (RightMenu1.Instance.inGroup || RightMenu1.Instance.inReaction)
        {
            RightMenuAnim.Play("RightSideRetract");
            RightMenu1.Instance.isOn = false;
            ScrolllistAnim.Play("ScrollListLeave");
            SelectorAnim.Play("SelectorAppear");
			if (RightMenu1.Instance.inGroup) {
				Initialize.sh.setCurrentState ("Group", true, true);
			}
			if (RightMenu1.Instance.inReaction) {
				Initialize.sh.setCurrentState ("Reaction", true, true);
			}
        }
    }
    /*
    public void OnMouseDown()
    {
        if (field.Equals("Reaction"))
            CosmicRanch.inReaction = true;
        Debug.Log("BUTTON DOWN : Field : "+field);
        OnClick();
    }
    */

}
