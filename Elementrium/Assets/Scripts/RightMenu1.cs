using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;
using UnityEngine.UI;

public class RightMenu1 : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;

	public GameObject Scrolllist;
    public Animator animScroll;

    public Animator animSelector;

	public ButtonListControl buttonListControl;
    public Text instructions;
    public static bool isOn;
    public static bool inFusion;
    public static bool inGroup;
    public static bool inReaction;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        animScroll = Scrolllist.GetComponent<Animator>();
        isOn = false;
        inFusion = false;
        inGroup = false;
        inReaction = false;
    }

    public void Play()
    {
        if (!isOn)
        {
            anim.Play("RightMenuSlideIn");
            isOn = true;
            StateHandler.setCurrentState("ActionBar", true, true);

        }
        else
        {
			if (!inGroup && !inReaction) {
				anim.Play ("RightSideRetract");
				isOn = false;
				StateHandler.setCurrentState ("MainGameScene", true, true);
			}
        }
    }
    public void InteractFusion() 
    {
        if (isOn)
        {
			instructions.text = "Pick two atoms to fuse!";
			anim.Play("RightSideRetract");
            animSelector.Play("SelectorAppear");
            isOn = false;
            inFusion = true;
			StateHandler.setCurrentState ("Fusion", true, true);
        }
    }
	public void InteractGroup()
	{
		if (isOn && !inGroup)
		{
			instructions.text = "Pick atoms to group into a molecule!";
			inGroup = true;
			buttonListControl.PopulateGroupList("Molecule");
            animScroll.Play("ScrollListEnter");
            StateHandler.setCurrentState("Group", true, true);
		}
	}
	public void InteractReaction()
	{
		if (isOn && !inReaction)
		{
			instructions.text = "Pick Triums to make a compound!";
			inReaction = true;
			buttonListControl.PopulateReactionList("Compound");
            animScroll.Play("ScrollListEnter");
            StateHandler.setCurrentState("Reaction", true, true);
		}
	}
}
