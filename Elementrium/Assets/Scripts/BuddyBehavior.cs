using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;
using Ranch;

namespace BudBehavior
{
    public class BuddyBehavior : MonoBehaviour
    {
        public static float velocity;
        public float xpos;
        public float ypos;
        public int ID;
        public static int IDc;
        public String triumformula;
        public GameObject buddy;
        public GameObject face;
        public int TriumID;
        public bool faceXYoff;
        public GameObject cr;
        public bool selected;
        public bool stayStill;
        public static bool selectable = false;

        public Animator faceAnim;

		public bool infoOn;

		public void Start()
		{
            cr = GameObject.FindGameObjectWithTag("CosmicRanch");
            if (IDc == 0)
                IDc = 1;
            else
                IDc++;

            ID = IDc;

			selected = false;
            infoOn = false;
			buddy = gameObject;
			System.Random rnd = new System.Random();
            float xdir = rnd.Next(0, 66);
            xdir -= 33;
            float xvel = Math.Abs(xdir);

            float yvel = (float) Math.Sqrt(1250 - (xvel * xvel));

            if (!stayStill) // Use stayStill to prevent a buddy from moving around
			    GetComponent<Rigidbody2D>().velocity = new Vector2(xdir, yvel);

			GetComponent<Rigidbody2D>().angularVelocity = 10;
            faceAnim = buddy.GetComponentInChildren<Animator>();

            faceXYoff = false; //face is on
		}

		public void Update()
		{
            xpos = GetComponent<Transform>().position.x;
            ypos = GetComponent<Transform>().position.y;

			if (selected)
			{
				buddy.GetComponent<SpriteRenderer>().color = new Color32(40, 255, 20, 255);
			}
			else
			{
				buddy.GetComponent<SpriteRenderer>().color = Color.white;
			}

            if (TopMenu1.Instance.faceIsOn) 
            {
                if (faceXYoff) {
                    faceXYoff = false;
                    face.SetActive(true);
                }
            } 
            else if  (!TopMenu1.Instance.faceIsOn) 
            {
                if (!faceXYoff) {
                    faceXYoff = true;
                    face.SetActive(false);
                }
            }

            int n = UnityEngine.Random.Range(1, 150);
            Debug.Log("Random Number: " + n);
            if (n == 1) {
                faceAnim.SetTrigger("blink");
            }
		}

        private void OnCollisionEnter2D(Collision2D collision)
        {
            faceAnim.SetTrigger("bump");
        }

        private void OnMouseDown()
        {
            SoundManager.Instance.PlaySoundSix();
            if (selectable) // A fusion/grouping/reaction is taking place, select the triums
            {
                selected = !selected;
                cr.GetComponent<CosmicRanch>().getSelected();
            } 
            else 
            {
                /*
                if (!infoOn)
                {
                    infoDisplayAnim.Play("TriumInfoPanelEnter");
                    infoOn = true;
                } else {
                    deleteAnim.Play("DeleteOptionExit");
                    TriumInfoDisplay.deleteOn = false;
                    infoDisplayAnim.Play("TriumInfoPanelExit");
                    infoOn = false;
				}
				*/
            }
        }

        public BuddyBehavior(Buddy buddy)
        {
            //xpos = 1f;
            velocity = buddy.getVelocity();
            xpos = buddy.getxpos();
            ypos = buddy.getypos();
            ID = buddy.getID();
            triumformula = buddy.getName();
            faceXYoff = buddy.getFace();
            selected = buddy.getSelect();
        }
    }
}
