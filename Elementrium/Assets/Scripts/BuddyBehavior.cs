using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

namespace BudBehavior
{
    public class BuddyBehavior : MonoBehaviour
    {
        public static float velocity;
        public static float xpos;
        public static float ypos;
        public static int ID;
        public static String triumname;
        public GameObject buddy;
        public static bool faceXYoff;
        public static bool selected;

		public void Start()
		{
			selected = false;
			buddy = gameObject;
			System.Random rnd = new System.Random();
			GetComponent<Rigidbody2D>().velocity = new Vector2(25, 25);
			GetComponent<Rigidbody2D>().angularVelocity = 10;
		}

		public void FixedUpdate()
		{

		}

		public void Update()
		{


			if (selected)
			{
				buddy.GetComponent<SpriteRenderer>().color = new Color32(40, 255, 20, 255);
			}
			else
			{
				buddy.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}

        private void OnMouseDown()
        {
            selected = !selected;
        }

        public BuddyBehavior(Buddy buddy)
        {
            //xpos = 1f;
            velocity = buddy.getVelocity();
            xpos = buddy.getxpos();
            ypos = buddy.getypos();
            ID = buddy.getID();
            triumname = buddy.getName();
            faceXYoff = buddy.getFace();
            selected = buddy.getSelect();
        }
    }
}
