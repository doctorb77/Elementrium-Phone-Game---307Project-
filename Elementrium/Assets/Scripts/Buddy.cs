using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts {

    public class Buddy:MonoBehaviour
    {
        public float velocity;
        public float xpos;
        public float ypos;
        public int id;
        public String triumname;
        public GameObject buddy;
        public ArrayList faceXYoff;
        public Boolean selected;

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
                buddy.GetComponent<SpriteRenderer>().color = new Color32(40,255,20,255);
            } else
            {
                buddy.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        private void OnMouseDown()
        {
            selected = !selected;
        }

        public Buddy()
        {
            //buddy = gameObject;
        }

        public Buddy(GameObject trium)
        {
            //buddy = trium;
        }

        public Buddy(String trium)
        {
            buddy = (GameObject) Instantiate(Resources.Load("Prefab/Trium/" + trium));
        }

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
