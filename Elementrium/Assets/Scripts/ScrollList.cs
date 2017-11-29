using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Initialization;

public class ScrollList : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public ButtonListControl buttonListControl;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void ExitScroll()
    {
        if (RightMenu1.Instance.isOn && RightMenu1.Instance.inGroup)
        {
            RightMenu1.Instance.inGroup = false;
            anim.Play("ScrollListLeave");
            anim.Play("RightSideRetract");
			//Debug.Log ("before:" + Initialize.sh.getCurrentState().name);
			Initialize.sh.setCurrentState ("ActionBar", true, true);//active and visible
			//Debug.Log ("after:" +Initialize.sh.getCurrentState().name);
            //GameObject.Find("RightActivation").GetComponent<RightMenu1>().Play();
        }
        else if (RightMenu1.Instance.isOn && RightMenu1.Instance.inReaction)
        {
            RightMenu1.Instance.inReaction = false;
            anim.Play("ScrollListLeave");
            anim.Play("RightSideRetract");
			//Debug.Log ("before:" +Initialize.sh.getCurrentState().name);
			Initialize.sh.setCurrentState ("ActionBar", true, true);//active and visible
			//Debug.Log ("after:" +Initialize.sh.getCurrentState().name);
            //GameObject.Find("RightActivation").GetComponent<RightMenu1>().Play();
        }
    }

}
