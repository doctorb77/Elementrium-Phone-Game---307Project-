using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using BackpackObject;
using BudBehavior;
using Initialization;
using Ranch;
using Group;
using Reaction;
using UnityEngine;

public class Test : MonoBehaviour {

	public bool testRunning;
	public bool testMusicInitialization;
	public bool testMusicToggle;
	public bool testSoundFXInitialization;
	public bool testSoundFXToggle;
	public bool testFusion;
	public bool testGroup;
    public bool testReaction;

	//for testing action
	public bool rerunFusion;
    public bool infusion;

	public bool rerunGroup;
	public bool ingroup;
    public bool groupSuccess;

	public bool rerunReaction;
	public bool inreaction;
	public bool reactionSuccess;

	public string actionProduct;
	List<GameObject> selected;

	
    // Use this for initialization
	void Start () {
        Debug.Log("Starting Unit Test series:");
        testRunning = false;
        testMusicInitialization = false;
        testMusicToggle = false;
        testMusicInitialization = false;
        testSoundFXToggle = false;
        testFusion = false;
        testGroup = false;
        testReaction = false;

        infusion = false;
        ingroup = false;
        inreaction = false;

        rerunFusion = false;
        rerunGroup = false;
        rerunReaction = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (CosmicRanch.Instance.inFusion) {
            infusion = true;
            selected = CosmicRanch.Instance.getSelected();
        }
        if (CosmicRanch.Instance.inGrouping) {
            ingroup = true;
            selected = CosmicRanch.Instance.getSelected();
        }
        if (CosmicRanch.Instance.inReaction)
		{
            inreaction = true;
			selected = CosmicRanch.Instance.getSelected();
		}
        //Debug.Log("TEST SELECTED: " + CosmicRanch.Instance.inFusion);
        if (!testRunning) {
            testRunning = true;
            StartCoroutine(TestMusicInitialized());
            StartCoroutine(TestMusicToggle());
            StartCoroutine(TestSoundFXInitialized());
            StartCoroutine(TestSoundFXToggle());
            StartCoroutine(TestFusion());
            StartCoroutine(TestGroup());
            StartCoroutine(TestReaction());
        }
        if (rerunFusion)
		{
            rerunFusion = false;
            StopCoroutine(TestFusion());
            StartCoroutine(TestFusion());
		}
		if (rerunGroup)
		{
			rerunGroup = false;
			StopCoroutine(TestGroup());
			StartCoroutine(TestGroup());
		}
        if (rerunReaction)
		{
            rerunReaction = false;
            StopCoroutine(TestReaction());
			StartCoroutine(TestReaction());
		}
	}

    IEnumerator TestMusicInitialized () {
		Debug.Log("Testing Music Initialization:");
		Debug.Log("Checking music initialization status, expected boolean value: TRUE");
		if (TopMenu1.Instance.musicIsOn)
		{
            Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
        testMusicInitialization = true;
        yield break;
	}

    IEnumerator TestMusicToggle () {
        yield return new WaitUntil(() => testMusicInitialization == true);
		Debug.Log("Testing Music Toggle:");
		Debug.Log("Checking music toggle status. Turn off music, expected boolean value: FALSE");
        Debug.Log("Awaiting input...");
        bool currentMusicStatus = TopMenu1.Instance.musicIsOn;
        yield return new WaitUntil(() => currentMusicStatus != TopMenu1.Instance.musicIsOn);

		if (!TopMenu1.Instance.musicIsOn)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
		Debug.Log("Checking music toggle status. Turn on music, expected boolean value: TRUE");
        Debug.Log("Awaiting input...");
		currentMusicStatus = TopMenu1.Instance.musicIsOn;
		yield return new WaitUntil(() => currentMusicStatus != TopMenu1.Instance.musicIsOn);

		if (TopMenu1.Instance.musicIsOn)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
        testMusicToggle = true;
        yield break;
    }

	IEnumerator TestSoundFXInitialized()
	{
        yield return new WaitUntil(() => testMusicToggle == true);
		Debug.Log("Testing Sound effects Initialization:");
		Debug.Log("Checking sound effects initialization status, expected boolean value: TRUE");
        if (TopMenu1.Instance.soundFxIsOn)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
        testSoundFXInitialization = true;
		yield break;
	}

	IEnumerator TestSoundFXToggle()
	{
        yield return new WaitUntil(() => testSoundFXInitialization == true);
		Debug.Log("Testing Sound Effects Toggle:");
		Debug.Log("Checking sound effects toggle status. Turn off sound effects, expected boolean value: FALSE");
		Debug.Log("Awaiting input...");
        bool currentSoundStatus = TopMenu1.Instance.soundFxIsOn;
        yield return new WaitUntil(() => currentSoundStatus != TopMenu1.Instance.soundFxIsOn);

        if (!TopMenu1.Instance.soundFxIsOn)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
		Debug.Log("Checking sound effects toggle status. Turn on sound effects, expected boolean value: TRUE");
		Debug.Log("Awaiting input...");
        currentSoundStatus = TopMenu1.Instance.soundFxIsOn;
        yield return new WaitUntil(() => currentSoundStatus != TopMenu1.Instance.soundFxIsOn);

        if (TopMenu1.Instance.soundFxIsOn)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
        testSoundFXToggle = true;
		yield break;
	}

	IEnumerator TestFusion()
	{
        yield return new WaitUntil(() => testSoundFXToggle == true);
		Debug.Log("Testing Fusion:");
		Debug.Log("Checking Fusion. Select two atoms and run Fusion");
		Debug.Log("Awaiting Action...");

        yield return new WaitUntil(() => infusion == true);
        if (selected.Count != 2) {
            Debug.Log("INVALID INPUT, RESTARTING TEST");
            infusion = false;
            rerunFusion = true;
            yield break;
        }
		int Trium1ID = selected[0].GetComponent<BuddyBehavior>().TriumID;
		int Trium2ID = selected[1].GetComponent<BuddyBehavior>().TriumID;
        Debug.Log("Atom 1 ID: " + Trium1ID);
        Debug.Log("Atom 2 ID: " + Trium2ID);
        int sum = Trium1ID + Trium2ID;
        Debug.Log("Expected Atom ID: " + sum);
        if (sum == CosmicRanch.Instance.buddies[CosmicRanch.Instance.buddies.Count - 1].GetComponent<BuddyBehavior>().TriumID) {
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
        infusion = false;
        testFusion = true;
		yield break;
	}

    IEnumerator TestGroup()
    {
        yield return new WaitUntil(() => testFusion == true);
		Debug.Log("Testing Group:");
		Debug.Log("Checking Group. Select two triums and run Group");
		Debug.Log("Awaiting Action...");

		yield return new WaitUntil(() => ingroup == true);
		if (selected.Count != 2)
		{
			Debug.Log("INVALID INPUT, RESTARTING TEST");
			ingroup = false;
			rerunGroup = true;
			yield break;
		}
		string Trium1Name = selected[0].GetComponent<BuddyBehavior>().triumformula;
		string Trium2Name = selected[1].GetComponent<BuddyBehavior>().triumformula;
		Debug.Log("Atom 1 Name: " + Trium1Name);
		Debug.Log("Atom 2 Name: " + Trium2Name);
		if (Trium1Name == "H" && Trium2Name == "H")
		{
			Debug.Log("Expected Output: H2");
            actionProduct = "H2";
            Debug.Log("Actual Output: " + CosmicRanch.Instance.buddies[CosmicRanch.Instance.buddies.Count - 1].GetComponent<BuddyBehavior>().triumformula);
		}
		else
		{
			Debug.Log("INVALID INPUT, RESTARTING TEST");
			ingroup = false;
			rerunGroup = true;
			yield break;
		}
        CosmicRanch.Instance.AddBuddyToList();
        if (actionProduct == CosmicRanch.Instance.buddies[CosmicRanch.Instance.buddies.Count - 1].GetComponent<BuddyBehavior>().triumformula)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
		ingroup = false;
		testGroup = true;
		yield break;
    }

	IEnumerator TestReaction()
	{
        yield return new WaitUntil(() => testGroup == true);
		Debug.Log("Testing Reaction:");
		Debug.Log("Checking Reaction. Select two triums and run Reaction");
		Debug.Log("Awaiting Action...");

        yield return new WaitUntil(() => inreaction == true);
		if (selected.Count != 2)
		{
			Debug.Log("INVALID INPUT, RESTARTING TEST");
            inreaction = false;
            rerunReaction = true;
			yield break;
		}
		string Trium1Name = selected[0].GetComponent<BuddyBehavior>().triumformula;
		string Trium2Name = selected[1].GetComponent<BuddyBehavior>().triumformula;
		Debug.Log("Atom 1 Name: " + Trium1Name);
		Debug.Log("Atom 2 Name: " + Trium2Name);
        if ((Trium1Name == "H2" && Trium2Name == "O") || (Trium1Name == "O" && Trium2Name == "H2"))
		{
            Debug.Log("Expected Output: H2O");
			actionProduct = "H2O";
			Debug.Log("Actual Output: " + CosmicRanch.Instance.buddies[CosmicRanch.Instance.buddies.Count - 1].GetComponent<BuddyBehavior>().triumformula);
		}
		else
		{
			Debug.Log("INVALID INPUT, RESTARTING TEST");
            inreaction = false;
            rerunReaction = true;
			yield break;
		}
		CosmicRanch.Instance.AddBuddyToList();
		if (actionProduct == CosmicRanch.Instance.buddies[CosmicRanch.Instance.buddies.Count - 1].GetComponent<BuddyBehavior>().triumformula)
		{
			Debug.Log("TEST PASSED");
		}
		else
		{
			Debug.Log("TEST FAILED");
		}
        inreaction = false;
        testReaction = true;
		yield break;
	}
}
