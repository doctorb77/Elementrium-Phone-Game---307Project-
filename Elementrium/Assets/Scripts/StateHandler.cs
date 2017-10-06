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
        public int currentstate;
        public static List<State> states;

        void Start()
        {
            
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
            State sA = new State("Achievement");
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
            State sMGS = new State("Main Game Scene");
            states.Add(sMGS);
        }
        public static State GetCurrentState()
        {
           // State[] st = new State[12];
            //int counter = 0;
            foreach(State s in states)
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
    }
}
