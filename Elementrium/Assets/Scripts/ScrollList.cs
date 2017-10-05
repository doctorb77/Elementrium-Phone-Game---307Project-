using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour {

	public GameObject Menu;
	public Animator anim;
	public static bool inGroup;
    public static bool inReaction;

	// Use this for initialization
	void Start () {
		anim = Menu.GetComponent<Animator>();
        inGroup = false;
        inReaction = false;
	}

	public void InteractGroup()
	{
        if (RightMenu1.isOn && !inGroup)
		{
            anim.Play("ScrollListEnter");
			inGroup = true;
		}
	}
    public void InteractReaction()
	{
        if (RightMenu1.isOn && !inReaction)
		{
			anim.Play("ScrollListEnter");
            inReaction = true;
		}
	}
    public void ExitScroll() {
		if (RightMenu1.isOn && inGroup)
		{
			anim.Play("ScrollListLeave");
			inGroup = false;
		}
        else if (RightMenu1.isOn && inReaction)
		{
			anim.Play("ScrollListLeave");
            inReaction = false;
		}
    }

}
