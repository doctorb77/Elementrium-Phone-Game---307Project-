using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;
using BackpackObject;
using TriumObject;
using WormholeObject;
using GlossaryObject;
using Ranch;
using Assets.Scripts;


namespace Initialization
{

    public class StartGame : MonoBehaviour
    {

        public void EnterGame()
        {
            SceneManager.LoadScene("MainGameScene");
            StateHandler.setCurrentState("MainGameScene", true, true);

            Initialize.onStart();
        }


    }

    public static class Initialize
    {

        public static Backpack player;
        // public static Wormhole wormhole;
        public static GameObject wormhole;
        public static Glossary glossary;
        public static CosmicRanch ranch;
        public static List<Buddy> buddyList;

        // Other global variables go here


        public static void onStart()
        {
            // Initialize Backpack
            if (player == null) {
                initBackpack();
            }


            // Initialize Wormhole
            if (wormhole == null) {
               wormhole = new GameObject("wormhole"); 
            }

            //testHole th = new testHole(player);
            //wormhole.AddComponent(th.GetType());

            // Initialize Glossary
            //glossary = new Glossary(player);

            // Initialize CosmicRanch
            if (ranch == null) {
                ranch = new CosmicRanch();
            }

            if (buddyList == null) {
                buddyList = new List<Buddy>();
            }



            // Initialize ANY other global variables

        }

        private static void initBackpack() {
            // do stuff

            // TODO: Check if there is a saved data file

            // default backpack init
            player = new Backpack();



        }

    }

}
