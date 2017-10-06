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
        public int id;
        public String triumname;
        public GameObject buddy;
        public ArrayList faceXYoff;

        public Buddy()
        {

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
