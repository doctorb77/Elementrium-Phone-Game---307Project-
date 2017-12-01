using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ranch;
using BudBehavior;
using UnityEngine.UI;
using StateHandling;
using WormholeObject;
using BackpackObject;
using Initialization;

namespace Notification_Bar
{
    public class Notification : MonoBehaviour
    {

        bool intro, didFusion, didGrouping, didReaction, didAll;
        public static bool popQuizes;
        public static GameObject text;
        public static bool gateOpen;
        public static bool currShow;
        public static Queue<string> Notes = new Queue<string>();
        public static Queue<float> Times = new Queue<float>();
        public Notification noteImp;
        bool tutDone;
        //public Animator NotificationAnim;
        public static string sText;

        public object Notfication { get; private set; }

        // Use this for initialization
        void Start()
        {
            gateOpen = true;
            currShow = false;
            tutDone = true;
            noteImp = (new GameObject("SomeObjName")).AddComponent<Notification>();
            //StartCoroutine(welcomeTutorial());
        }

        public Notification()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (!currShow && Notes.Count > 0 && Times.Count > 0)
            {
                Debug.Log("Starting Coroutine Cycle with " + Notes.Count + " in queue");
                IEnumerator[] seq = new IEnumerator[Notes.Count];
                
                if (Notes.Count == 1)
                {
                    StartCoroutine(noteImp.Sequence(processTask(Notes.Dequeue(), Times.Dequeue())));
                }
                else if (Notes.Count == 2)
                {
                    StartCoroutine(noteImp.Sequence(processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue())));
                }
                else if (Notes.Count == 3)
                {
                    StartCoroutine(noteImp.Sequence(processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue())));
                }
                else if (Notes.Count == 4)
                {
                    StartCoroutine(noteImp.Sequence(processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue())));
                }
                else if (Notes.Count == 5)
                {
                    StartCoroutine(noteImp.Sequence(processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue()), processTask(Notes.Dequeue(), Times.Dequeue())));
                }

            }     
        }

        public IEnumerator Sequence(params IEnumerator[] sequence)
        {
            for (int i = 0; i < sequence.Length; ++i)
            {
                while (sequence[i].MoveNext())
                    yield return sequence[i].Current;
            }
        }

        public IEnumerator processTask(string s, float waitTime)
        {
            currShow = true;
            GameObject.FindGameObjectWithTag("Note").GetComponent<Text>().text = s;
            Debug.Log("Check A : " + GameObject.FindGameObjectWithTag("Note").GetComponent<Text>().text);
            yield return new WaitForSeconds(0.5f);
            GameObject.FindGameObjectWithTag("FactsNote").GetComponent<Animator>().Play("NotificationEnter");
            yield return new WaitForSeconds(waitTime);
            Debug.Log("Check B : " + GameObject.FindGameObjectWithTag("Note").GetComponent<Text>().text);
            GameObject.FindGameObjectWithTag("FactsNote").GetComponent<Animator>().Play("NotificationExit");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Size of Queue : " + Notes.Count);
        }

        IEnumerator welcomeTutorial()
        {
            Debug.Log("HERE");
            yield return new WaitForSeconds(0.5f);
            notifyT("Welcome to Elementrium! This cosmic workspace is where you will be able to make over a hundered different elements and molecules!", 5);
            yield return new WaitForSeconds(5.5f);
            notifyT("To get started, you can click the Wormhole, or the spinning purple portal at the bottom of the workspace.", 5);
            yield return new WaitForSeconds(5.5f);
            yield return new WaitUntil(() => Wormhole.tappedOnce == true);
            yield return new WaitForSeconds(0.5f);
            notifyT("Awesome, Now try opening up the right side menu and hitting fusion. There you can fuse two hydrogens together.", 5);
            yield return new WaitForSeconds(5.5f);
            yield return new WaitUntil(() => Initialize.player.getTrium(2) != null);
            yield return new WaitForSeconds(0.5f);
            notifyT("Nice! You made helium. Now try grouping two hydrogens together using the grouping function on the right menu.", 5);
            yield return new WaitForSeconds(5.5f);
            yield return new WaitUntil(() => Initialize.player.getTrium(100) != null);
            yield return new WaitForSeconds(0.5f);
            notifyT("Thats Hydrogen Gas! You also leveled up too. Now you can fuse Lithium. Try fusing some elements together to get Lithium.", 5);
            yield return new WaitForSeconds(5.5f);
            yield return new WaitUntil(() => Initialize.player.getTrium(3) != null);
            yield return new WaitForSeconds(0.5f);
            notifyT("Great! Now we can peform a reaction, check out the reaction tab and see what we need to make Lithium Hydride ( LiH ).", 5);
            yield return new WaitForSeconds(5.5f);
            yield return new WaitUntil(() => Initialize.player.getTrium(93) != null);
            yield return new WaitForSeconds(0.5f);
            notifyT("Well it seems you understand the basics. Check out the glossary by clicking on your level and then the book to see all of the facts you've unlocked.", 5);
            yield return new WaitForSeconds(5.5f);

            tutDone = true;
            popQuizes = true;
        }

        public void notifyKey(string s, float time)
        {
                StartCoroutine(processTask(s,time));
        }

        public static void notify(string s, float time)
        {
            if (Notes.Count < 5)
            {
                Notes.Enqueue(s);
                Debug.Log("Enqueued : " + s);
                Times.Enqueue(time);
            }
        }

        public void notifyT(string s, float time)
        {
            text.GetComponent<Text>().text = s;
            Debug.Log("Assigned text : " + s);
            StartCoroutine(processTask(s,time));
        }

        public void setText(string s)
        {
            sText = s;
        }
    }
}