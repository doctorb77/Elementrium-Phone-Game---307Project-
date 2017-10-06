using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using Assets.Scripts;
using States;

namespace StateHandling
{
    public class StateHandler : MonoBehaviour
    {
        public static State currentstate = null;
		public static List<State> states = new List<State>();

        void Start()
        {
			State sAB = new State("ActionBar");
			states.Add(sAB);
			State sQ = new State("Quiz");
			states.Add(sQ);
			State sSB = new State("StatsBar");
			states.Add(sSB);
			State sS = new State("Settings");
			states.Add(sS);
			State sA = new State("Achievements");
			states.Add(sA);
			State sMM = new State("MainMenu");
			states.Add(sMM);
			State sGl = new State("Glossary");
			states.Add(sGl);
			State sM = new State("Menu");
			states.Add(sM);
			State sZI = new State("ZoomIn");
			states.Add(sZI);
			State sF = new State("Fusion");
			states.Add(sF);
			State sGr = new State("Group");
			states.Add(sGr);
			State sR = new State("Reaction");
			states.Add(sR);
			State sMGS = new State("MainGameScene");
			states.Add(sMGS);
			State sRS = new State("RewardScene");
			states.Add(sRS);
			//locks everything until display goes away
        }

        public StateHandler()
        {
            State sAB = new State("ActionBar");
            states.Add(sAB);
            State sQ = new State("Quiz");
            states.Add(sQ);
            State sSB = new State("StatsBar");
            states.Add(sSB);
            State sS = new State("Settings");
            states.Add(sS);
            State sA = new State("Achievements");
            states.Add(sA);
            State sMM = new State("MainMenu");
            states.Add(sMM);
            State sGl = new State("Glossary");
            states.Add(sGl);
            State sM = new State("Menu");
            states.Add(sM);
            State sZI = new State("ZoomIn");
            states.Add(sZI);
            State sF = new State("Fusion");
            states.Add(sF);
            State sGr = new State("Group");
            states.Add(sGr);
            State sR = new State("Reaction");
            states.Add(sR);
            State sMGS = new State("MainGameScene");
            states.Add(sMGS);
            State sRS = new State("RewardScene");
            states.Add(sRS);
                //locks everything until display goes away
        }
        public static State GetCurrentState()
        {
            // State[] st = new State[12];
            //int counter = 0;
            foreach (State s in states)
            {
                if (s.isActive == true)
                {
                    //st[counter] = s;
                    // counter++;
                    return s;
                }
            }
            return null;
        }
        public static void setCurrentState(String name, Boolean visible, Boolean active)
        {
            if (currentstate != null)
            {
                currentstate.isActive = false;
                currentstate.isVisible = false;
                currentstate = null;
            }
            foreach (State s in states)
            {
                //Console.WriteLine(s.name);
                print(s.name);
                if (s.name == name)
                {
                    s.isVisible = visible;
                    s.isActive = active;

                }
                if (s.isActive == true)
                {
                    currentstate = s;
                }
            }
        }
    }
}
