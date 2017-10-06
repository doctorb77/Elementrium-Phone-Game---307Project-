using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Assets.Scripts;
using StateHandling;
using States;
//using Fusion;

public class AtomSelect : MonoBehaviour {

    public GameObject buddy;
    public int buddyselected = 0;

    //public Animator anim;
    //backpack and key of hashmap from backpack (key is id on db table)
    //fusion runs after clicking fusion button AND an atom


	// Use this for initialization
	void Start () {
		
	}
	
    public void InteractAtom()
    {
        if (EventSystem.current.currentSelectedGameObject.name.Contains("AtomButton"))
        {
            State current = StateHandler.GetCurrentState();

            if (current.name == "Fusion")
            {
                //confirm button work, click, call FusionHandler.fuse()
            }
            if (current.name == "Group")
            {
                //grouping function
            }
            if (current.name == "Reaction")
            {
                //reaction function
            }
            if (current.name == "Main Game Scene")
            {
                //zoomin only time
            }

            //StateHandler sh = new StateHandler();
            
            Buddy b = new Buddy();
            //find in cosmic ranch arraylist = EventSystem.current.currentSelectedGameObject.name;
            //b.buddy.name = //currentbuddy

            
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
