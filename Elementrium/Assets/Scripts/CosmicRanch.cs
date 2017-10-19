﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
using Initialization;
using Reaction;
using WormholeObject;
using BudBehavior;
using BackpackObject;

namespace Ranch {
    public class CosmicRanch : MonoBehaviour
    {
        public Backpack bp;
        public SpriteRenderer background;
        public SpriteRenderer space;
        public SpriteRenderer rim;
        public GameObject Wormhole;
        public int colorChoice;
        public Color32 color;
        public Color32 rimColor;
        public List<GameObject> buddies = new List<GameObject>();
        public List<GameObject> selUpdate;
        public GameObject[] buddos;
        public static bool inReaction, inFusion, inGrouping;

        // Use this for initialization

        void Start()
        {
            //bp = Wormhole.GetComponent<Wormhole>().bp;
            // load all buddies into system
            AddBuddyToList();

            if (colorChoice == 0)
            {
                color = new Color32(120, 45, 212, 255);  // SetColor("_Color", Color.blue);
                rimColor = new Color32(103, 104, 255, 255);
            }
            else
            {

            }
            //Debug.Log("RESTARTIN' YALL");
            if (colorChoice == 0)
            {
                background.material.SetColor("_Color", Color.black);
            }
            else
            {
                background.material.SetColor("_Color", color);
            }

            // Go through Global buddy list
            // Create new buddy objects and populate the scene
            // make a extra method
            //print(Initialize.buddyList.Count);

            for (int i = 0; i < Initialize.buddyList.Count; i++)
            {
                print(Initialize.buddyList[i] == null);
                print(Initialize.buddyList[i].getName());
                BuddyBehavior bm = new BuddyBehavior(Initialize.buddyList[i]);
                bm.transform.localPosition = new Vector3(Initialize.buddyList[i].getxpos(), Initialize.buddyList[i].getypos(), -1f);

                //Initialize.buddyList[i].transform.localPosition = new Vector3(Initialize.buddyList[i].getxpos(), Initialize.buddyList[i].getypos(), -1);
            }

            //foreach(Buddy buddy in Initialize.buddyList.ToArray()) 
            //{

            //}
        }

        // Update is called once per frame
        void Update()
        {
            if (inReaction)
            {
                //List<string> reactants = new List<string> { "H2", "O" };
                //List<string> 
                bool done = ReactionHandler.reactCurrent(getSelected(), new List<string>() { "H2", "O" }, new List<int>() { 1, 1 }, new List<string>() { "H2O" }, new List<int>() { 1 });
                if (done)
                {
                    deselectAll();
                    makeBuddiesSelectable(false);
                    inReaction = false;
                }

                //Debug.Log("DONE : "+done);
            }
            //space.color = color;
            //rim.color = rimColor;
        }

        public void ChangeColor()
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "Blue":
                    colorChoice = 0;
                    color = new Color32(120, 45, 212, 170);
                    rimColor = new Color32(43, 196, 232, 255);
                    break;
                case "Green":
                    colorChoice = 1;
                    color = new Color32(6, 193, 81, 170);
                    rimColor = new Color32(82, 236, 115, 255);
                    break;
                case "Yellow":
                    colorChoice = 2;
                    color = new Color32(255, 185, 0, 170);
                    rimColor = new Color32(255, 255, 114, 255);
                    break;
                case "Purple":
                    colorChoice = 3;
                    rimColor = new Color32(172, 77, 193, 255);
                    color = new Color32(215, 0, 111, 170);
                    break;
                case "Orange":
                    colorChoice = 4;
                    rimColor = new Color32(218, 177, 90, 255);
                    color = new Color32(255, 121, 0, 170);
                    break;
            }
            space.color = color; // SetColor("_Color", color);
            rim.color = rimColor; // SetColor("_Color", color);
        }
		public void AddBuddyToList()
		{
            /*
			Initialize.buddyList.Add(buddy);
			printList();
            */
            //buddies.Add(buddy);
            buddies.Clear();
            buddies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Buddy"));
        }
		public void RemoveBuddyFromList(GameObject buddy)
		{
            /*
			Initialize.buddyList.Remove(buddy);
			printList();
            */
            bool removed = buddies.Remove(buddy);
            //Debug.Log("Removed : " + removed);

            GameObject.Destroy(buddy);
        }
        public void setReaction(bool inR)
        {
            inReaction = inR;
        }

        public List<GameObject> getSelected()
        {
            List<GameObject> selected = new List<GameObject>();

            foreach (GameObject buddy in buddies)
            {
                if (buddy.GetComponent<BuddyBehavior>().selected == true)
                    selected.Add(buddy);
            }

            selUpdate = selected;
            return selected;
        }
        
        // Deselects all of the buddies in the cosmic ranch
        public void deselectAll()
        {
            foreach (GameObject buddy in buddies)
                buddy.GetComponent<BuddyBehavior>().selected = false;
        }

        public void makeBuddiesSelectable(bool selectable)
        {
                BuddyBehavior.selectable = selectable;
        }


        public void printList(List<GameObject> l) {

            string s = "";

            foreach (GameObject buddy in l)
            {
                s += buddy.GetComponent<BuddyBehavior>().ID + " ";
            }

            Debug.Log(s);
        }

		public void printList()
		{
			foreach (var item in buddies)
			{
				print(item.name);
			}
		}
    }
}
