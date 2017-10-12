using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
using Initialization;
using BudBehavior;

namespace Ranch {
    public class CosmicRanch : MonoBehaviour
    {

        public SpriteRenderer background;
        public SpriteRenderer space;
        public SpriteRenderer rim;
        public GameObject Wormhole;
        public int colorChoice;
        public Color32 color;
        public Color32 rimColor;
        // public List<GameObject> buddies = new List<GameObject>();

        // Use this for initialization

        void Start()
        {
            if (colorChoice == 0)
            {
                color = new Color32(120, 45, 212, 255);  // SetColor("_Color", Color.blue);
                rimColor = new Color32(103, 104, 255, 255);
            }
            else
            {

            }
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
		public void AddBuddyToList(Buddy buddy)
		{

			Initialize.buddyList.Add(buddy);
			//printList();
		}
		public void RemoveBuddyFromList(Buddy buddy)
		{
			Initialize.buddyList.Remove(buddy);
			//printList();
		}

		public void printList()
		{
			foreach (var item in Initialize.buddyList)
			{
				print(item.buddy.name);
			}
		}
    }
}
