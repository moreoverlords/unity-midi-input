using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputParser : MonoBehaviour {

	public TimeSpan ChordToleranceTime; // max tolerable time to complete a chord
	public int MinAttack = 60; // this is the dividing line b/w movement and attack
	public List<int> NoteCache;
	public DateTime ChordStart;

	// Use this for initialization
	void Start () {
		NoteCache = new List<int> ();
		ChordStart = null;
		ChordToleranceTime = new TimeSpan (100000); // 10ms
	}
	
	// Update is called once per frame
	void Update () {
		if (ChordStart + ChordToleranceTime < System.DateTime.UtcNow) {
			// chord tolerance has past, clear chord

		}
		// check keys for inputs
		for (var i = 0; i < 128; i++) {
			if (MidiInput.GetKeyDown (i)) {
				if (ChordStart == null) {
					ChordStart = System.DateTime.UtcNow;
					NoteCache.Clear ();
					NoteCache.Add (i);
				}
			}
		}
	}

	void EvaluateChord(){}
}
