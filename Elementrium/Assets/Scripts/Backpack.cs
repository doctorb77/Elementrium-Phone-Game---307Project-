using UnityEngine;
using System.Collections;
using TriumObject;
using System.Collections.Generic;
using Reaction;

namespace BackpackObject 
{

    public class Backpack 
    {
        public static int level = 1;
        public static int exp;
        private Hashtable bp;
                                       //1,   2,  3,   4
        public static int[] expLevels = {0,140,700,5500,10900,2000000000};
        public static List<int> reactionIDs = new List<int>();
        public static List<int> fusionIDs = new List<int>();
        public static int[] elementCap = {0,2,3,8,13,92}; // Li, O, O
        public static bool[] unlockedElement = new bool[93];
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

            for (int i = 0; i < 93; i++)
            {
                unlockedElement[i] = false;
            }

            // Set 0 (none) and hydrogen(1) to true
            unlockedElement[0] = false;
            unlockedElement[1] = true;
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

        public void updateTiers(List<string> IDS)
        {
            foreach (string triumID in IDS) {
                string triumName = "";
                int atomID = 1;
                int tID = ReactionHandler.getInfo(triumID, out triumName, out atomID);
                Trium t = (Trium)bp[tID];
                t.increaseTier();
                Debug.Log("Increased Tier for " + triumID + " to " + t.getTier());
            }
        }

        public void updateTiers(List<int> IDS)
        {
            foreach (int triumID in IDS)
            {
                Trium t = (Trium)bp[triumID];
                t.increaseTier();
                Debug.Log("Increased Tier for " + t.getName() + " to " + t.getTier());
            }
        }

        // reactionID = reaction table id, level = stage level, type : 0 = fusion, 1 = grouping, 2 = reaction
        public static void handleExp(int reactionID, int level, int type)
        {
            int rBase = 200;
            int gBase = 50;
            int fBase = 100;

            if ((reactionIDs.Contains(reactionID) && (type == 1 || type == 2)) || (fusionIDs.Contains(reactionID) && type == 0))
            {
                rBase /= 10;
                gBase /= 10;
                fBase /= 10;

                if (type == 0)
                    gainExp(level * fBase);
                else if (type == 1)
                    gainExp(level * gBase);
                else if (type == 2)
                    gainExp(level * rBase);
            } else
            {
                if (type == 0)
                    gainExp(level * fBase);
                else if (type == 1)
                    gainExp(level * gBase);
                else if (type == 2)
                    gainExp(level * rBase);

                if (type == 1 || type == 2)
                    reactionIDs.Add(reactionID);
                else
                    fusionIDs.Add(reactionID);
            }
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

            //Debug.Log("LEVEL UPDATE : " + add + " exp added, " + level + " is level, " + " Max element = " + maxElement);

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
			//Debug.Log (bp.Contains (tableID));
            return (bp.Contains(tableID)) ? (Trium)bp[tableID] : null;
        }

    }

}
