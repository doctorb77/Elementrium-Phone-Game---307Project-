using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StateHandling;
using GlossaryObject;
using Ranch;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Initialization;

public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    private Text myText;
    public string field; // Group, Reaction, None
    public Animator GlossaryAnim;
    public Animator RightMenuAnim;
    public Animator ScrolllistAnim;
    public Animator SelectorAnim;
    public CosmicRanch ranch;

    public void SetText(string textString)
    {
        myText.text = textString;
    }
    public void OnClick()
    {
        Debug.Log("TESTWETF");
        if (SceneManager.GetActiveScene().name == "MainGameScene")
        {
            Debug.Log("BUTTON CLICKED");
            GameObject rightMenu = GameObject.Find("RightActivation");
            rightMenu.GetComponent<RightMenu1>().Play();
        }
        else if (SceneManager.GetActiveScene().name == "Glossary")
        {
            if (!Glossary.displayOpen)
            {
                GlossaryAnim.Play("GlossaryInfoDisplayPopUp");

            }
        }


    }
    public void StartSelector()
    {
        if (RightMenu1.inGroup || RightMenu1.inReaction)
        {
            RightMenuAnim.Play("RightSideRetract");
            RightMenu1.isOn = false;
            ScrolllistAnim.Play("ScrollListLeave");
            SelectorAnim.Play("SelectorAppear");
			if (RightMenu1.inGroup) {
				Initialize.sh.setCurrentState ("Group", true, true);
			}
			if (RightMenu1.inReaction) {
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
