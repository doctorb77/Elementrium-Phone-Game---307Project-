using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Assets.Scripts;
using StateHandling;
using States;
//using Fusion;
using UnityEngine.UI;
using Ranch;

public class TriumSelect : MonoBehaviour {

    public GameObject buddy;
    public CosmicRanch ranch;
    public Animator SelectorAnim;
    public int buddyselected = 0;

    //public Animator anim;
    //backpack and key of hashmap from backpack (key is id on db table)
    //fusion runs after clicking fusion button AND an atom


	// Use this for initialization
	void Start () {
		
	}
	
    public void InteractTrium()
    {
        /*
        if (EventSystem.current.currentSelectedGameObject.name.Contains("AtomButton"))
        {
            
        }
        */
        State current = StateHandler.GetCurrentState();

        if (RightMenu1.inFusion)
		{
            if (EventSystem.current.currentSelectedGameObject.name == "ConfirmButton")
			{
                ranch.setFusion(true);
                SelectorAnim.Play("SelectorDisappear");
                RightMenu1.inFusion = false;
			}
			else if (EventSystem.current.currentSelectedGameObject.name == "CancelButton")
			{
				SelectorAnim.Play("SelectorDisappear");
				ranch.deselectAll();
                RightMenu1.inFusion = false;
			}
		}
		
        if (RightMenu1.inGroup)
		{
            if (EventSystem.current.currentSelectedGameObject.name == "ConfirmButton")
			{
                ranch.setGroup(true);
                SelectorAnim.Play("SelectorDisappear");
                RightMenu1.inGroup = false;
			}
			else if (EventSystem.current.currentSelectedGameObject.name == "CancelButton")
			{
				SelectorAnim.Play("SelectorDisappear");
				ranch.deselectAll();
				RightMenu1.inGroup = false;
			}
		}
		
        if (RightMenu1.inReaction)
		{
			if (EventSystem.current.currentSelectedGameObject.name == "ConfirmButton")
			{
				ranch.setReaction(true);
                SelectorAnim.Play("SelectorDisappear");
                RightMenu1.inReaction = false;
			}
			else if (EventSystem.current.currentSelectedGameObject.name == "CancelButton")
			{
				SelectorAnim.Play("SelectorDisappear");
                ranch.deselectAll();
                RightMenu1.inReaction = false;
			}
		}
        /*
		if (current.name == "Main Game Scene")
		{
			//zoomin only time
			StateHandler.setCurrentState("ZoomIn", true, true);
		}
        */
		//StateHandler sh = new StateHandler();

		//Buddy b = new Buddy();
		//find in cosmic ranch arraylist = EventSystem.current.currentSelectedGameObject.name;
		//b.buddy.name = //currentbuddy


	}

	// Update is called once per frame
	void Update () {
		
	}
}
