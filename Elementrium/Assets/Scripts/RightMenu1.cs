using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;
using UnityEngine.UI;
using Initialization;

public class RightMenu1 : MonoBehaviour
{

    public static RightMenu1 Instance { get { return instance; } }
    private static RightMenu1 instance;

    public GameObject Menu;
    public Animator anim;

	public GameObject Scrolllist;
    public Animator animScroll;

    public Animator animSelector;

	public ButtonListControl buttonListControl;
    public Text instructions;
    public bool isOn;
    public bool inFusion;
    public bool inGroup;
    public bool inReaction;

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
            Initialize.sh.setCurrentState("ActionBar", true, true);

        }
        else
        {
			if (!inGroup && !inReaction) {
				anim.Play ("RightSideRetract");
				isOn = false;
                Initialize.sh.setCurrentState ("MainGameScene", true, true);
			}
        }
    }
    public void InteractFusion() 
    {
        if (isOn)
        {
			//instructions.text = "Pick two atoms to fuse!";
			anim.Play("RightSideRetract");
            animSelector.Play("SelectorAppear");
            isOn = false;
            inFusion = true;
            Initialize.sh.setCurrentState ("Fusion", true, true);
        }
    }
	public void InteractGroup()
	{
		if (isOn && !inGroup)
		{
			//instructions.text = "Pick atoms to group into a molecule!";
			inGroup = true;
			buttonListControl.PopulateGroupList("Molecule");
            animScroll.Play("ScrollListEnter");
            Initialize.sh.setCurrentState("Group", true, false);//visible, not active
		}
	}
	public void InteractReaction()
	{
		if (isOn && !inReaction)
		{
			//instructions.text = "Pick Triums to make a compound!";
			inReaction = true;
			buttonListControl.PopulateReactionList("Compound");
            animScroll.Play("ScrollListEnter");
            Initialize.sh.setCurrentState("Reaction", true, false);//visible not active
		}
	}
}
