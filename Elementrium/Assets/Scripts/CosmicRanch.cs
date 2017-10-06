using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
using Initialization;
using BudBehavior;

namespace Ranch
{
    public class CosmicRanch : MonoBehaviour
    {

        public SpriteRenderer background;
        public int colorChoice;
        public Color32 color;
        //public List<Buddy> buddies = Initialize.buddyList;

        // Use this for initialization


        void Start()
        {
            Debug.Log("RESTARTIN' YALL");
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
            print(Initialize.buddyList.Count);

            for (int i = 0; i < Initialize.buddyList.Count; i++) {
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

        }

        public void ChangeColor()
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "Black":
                    colorChoice = 1;
                    color = new Color32(0, 0, 0, 255);
                    break;
                case "Red":
                    colorChoice = 2;
                    color = new Color32(58, 0, 0, 255);
                    break;
                case "Purple":
                    colorChoice = 3;
                    color = new Color32(38, 0, 58, 255);
                    break;
                case "Blue":
                    colorChoice = 4;
                    color = new Color32(0, 0, 58, 255);
                    break;
                case "Green":
                    colorChoice = 5;
                    color = new Color32(0, 30, 3, 255);
                    break;
            }
            background.material.SetColor("_Color", color);
        }
        public void AddBuddyToList(Buddy buddy)
        {
            
            Initialize.buddyList.Add(buddy);
            //printList();
        }
        public void RemoveBuddyFromList (Buddy buddy) 
        {
            Initialize.buddyList.Remove(buddy);
            //printList();
        }

        public void printList() {
            foreach (var item in Initialize.buddyList)
            {
                print(item.buddy.name);
            }
        }
    }
}
