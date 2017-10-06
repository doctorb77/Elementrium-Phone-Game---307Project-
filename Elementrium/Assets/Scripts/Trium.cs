using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriumObject
{
    
    public class Trium
    {
        
        //private int tableID;    // The row of the Trium in the database
        private string name;    // the name of the Trium
        private int atomicNumber;  // The atomic number of the element (if applicable)
        private int count;      // How many of this Trium the user has
        private int tier;       // The fact-tier the user has achieved
                                // This is 0 through 3, inclusive



        public Trium(string name, int atomicNumber) {
            if (name == null) {
                return;
            }

            this.count = 0;
            this.tier = 0;
            this.name = name;
            this.atomicNumber = atomicNumber;

        }


        /*
         * Old Trium constructor

          
        // Trium constructor. Used to create Trium objects
        public Trium () {
            //this.tableID = tableID;
            this.count = 0;
            this.tier = 0;
        }
        
        */

        public string getName() {
            return this.name;
        }

        public int getAtomicNumber() {
            return this.atomicNumber;
        }

        // Returns the count of Triums
        public int getCount() {
            return this.count;
        }


        // Increases the count of Triums
        public void increaseCount() {
            this.count++;
        }


        // Decreases the count of Triums, if possible
        public void decreaseCount() {
            count = (count == 0) ? 0 : count--;
        }


        // Sets the amount of Triums the user has, used during initialization
        public void setCount(int amount) {
            this.count = (amount >= 0) ? amount : 0;
        }


        // Returns the Tier of this Trium.
        public int getTier() {
            return this.tier;
        }


        // Method for increasing the Tier of this Trium, if possible
        public void increaseTier() {
            this.tier = (tier == 3) ? 3 : tier++;
        }


        
    }

}
