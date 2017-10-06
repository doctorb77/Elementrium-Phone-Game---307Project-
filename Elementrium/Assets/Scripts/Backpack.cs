using UnityEngine;
using System.Collections;
using TriumObject;

namespace BackpackObject 
{

    public class Backpack 
    {
        private int level;
        private int exp;
        private Hashtable bp;
        // private int[] levelSteps;
        private int[] levelRange;

        // TODO: IMPLEMENT
        //private AchievementList ach;


        public Backpack() {
            this.bp = new Hashtable();
            this.level = 1;
            this.exp = 0;
            levelRange = new int[1];


            //this.ach = new AchievementList();
            
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

        // Returns the level of the user.
        public int getLevel() {
            return this.level;
        }

        // Increases the level of the user by 1.
        public void increaseLevel() {
            // TODO: Finalize leveling logic
            level = (level == levelRange.Length - 1) ? level : level++;
        }

        // Returns the amount of Exp the player has accrued in this level.
        public int getExp() {
            return this.exp;
        }

        // Increases the player's Exp by the given amount
        // Also checks to see if this pushes them past a level-up threshold
        public void increaseExp(int amount) {
            // TODO: IMPLEMENT EXP LOGIC
        }

        // Gets a specific trium from the backpack if it exists
        public Trium getTrium(int tableID) {
            return (bp.Contains(tableID)) ? (Trium)bp[tableID] : null;
        }

    }

}
