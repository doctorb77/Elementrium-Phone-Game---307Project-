using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{

    public class Buddy
    {
        public float velocity;
        public float xpos;
        public float ypos;
        public int ID;
        public String triumname;
        public GameObject buddy;
        public bool faceXYoff;
        public bool selected;

        //public void Start()
        //{
            //selected = false;
            //buddy = gameObject;
        //}

        public void Update()
        {


            if (selected)
            {
                buddy.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                buddy.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        private void OnMouseDown()
        {
            //selected = !selected;
        }

        public Buddy(float v, float x, float y, int id, String name, GameObject b, bool f, bool s)
        {
            velocity = v;
            xpos = x;
            ypos = y;
            ID = id;
            triumname = name;
            buddy = b;
            faceXYoff = f;
            selected = s;
        }

        public string getName() {
            return triumname;
        }
        public int getID () {
            return ID;
        }
        public GameObject getObject () {
            return buddy;
        }
        public bool getFace() {
            return faceXYoff;
        }
        public bool getSelect() {
            return selected;
        }
        /*
        public Buddy(GameObject trium)
        {
            //buddy = trium;
        }

        public Buddy(String trium)
        {
            buddy = (GameObject)Instantiate(Resources.Load("Prefab/Trium/" + trium));
        }
        */
        public float getVelocity()
        {
            return this.velocity;
        }
        public void setVelocity(float velocity2)
        {
            this.velocity = velocity2;
        }
        public float getxpos()
        {
            return this.xpos;
        }
        public void setxpos(float xpos2)
        {
            this.xpos = xpos2;
        }
        public float getypos()
        {
            return this.ypos;
        }
        public void setypos(float ypos2)
        {
            this.ypos = ypos2;
        }
    }

}