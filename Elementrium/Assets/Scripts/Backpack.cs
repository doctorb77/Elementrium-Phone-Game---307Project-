using UnityEngine;
using System.Collections;
using TriumObject;

namespace BackpackObject 
{

    public class Backpack 
    {
        public static int level = 1;
        public static int exp;
        private Hashtable bp;
                                       //1,   2,  3,   4
        public static int[] expLevels = {0,200,500,1000};
        public static int[] elementCap = {0,2,3, 8, 8 }; // Li, O, O
        public static int maxElement = 2; // Default to oxygen for testing.
        // private int[] levelSteps;
        private int[] levelRange;

        // TODO: IMPLEMENT
        //private AchievementList ach;


        public Backpack() {
            this.bp = new Hashtable();
            level = 1;
            exp = 0;
            levelRange = new int[1];


            //this.ach = new AchievementList();   
        }

        public static double getLevelPercentage()
        {
            // Used mainly for expBar
            double totalExpNeeded = expLevels[level] - expLevels[level - 1];
            double expGained = exp - expLevels[level - 1];
            double percentage = expGained / totalExpNeeded;
            return percentage;
        }

        public static void gainExp(int add)
        {
            exp += add;

            if (exp > expLevels[level])
            {
                level++;
                maxElement = elementCap[level];
                gainExp(0); // In some weird case where 2 levels are gained at once :/
            }

        }

        // Return the backpack hashtable
        public Hashtable getBP() {
			return bp;
		}

        // Add the Trium to the backpack if they don't already have it
        // If they do have it, increase the Trium's count by 1
        public void addToBackpack(int tableID, string name, int atomicNumber) {
            if (!bp.Contains(tableID)) {
				Trium t = new Trium(name, atomicNumber);
				t.increaseCount();
				bp.Add(tableID, t);

            } else {
                Trium t = (Trium)bp[tableID];
                t.increaseCount();
            }
        }

        // Inserts Trium into backpack if it isn't there w/ custom amount
        // Used during Initialization
        public void addToBackpack(int tableID, string name, int atomicNumber, int amount) {
            if (!bp.Contains(tableID)) {
                Trium t = new Trium(name, atomicNumber);
                t.setCount(amount);
                bp.Add(tableID, t);
            }
        }

        // Gets a specific trium from the backpack if it exists
        public Trium getTrium(int tableID) {
            return (bp.Contains(tableID)) ? (Trium)bp[tableID] : null;
        }

    }

}
