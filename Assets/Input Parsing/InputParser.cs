using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputParser : MonoBehaviour {

	public TimeSpan ChordToleranceTime; // max tolerable time to complete a chord
	public int MinAttack = 60; // this is the dividing line b/w movement and attack
	public List<int> NoteCache;
	public DateTime? ChordStart;
	private int _LeftNote;
	private int _DownNote;
	private int _UpNote;
	private int _RightNote;

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
			if (NoteCache.Count > 0) {
				EvaluateChord();
			}
			ChordStart = null;
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

	/// <summary>
	/// Considers the current state of NoteCache as a chord, and ships it to the CharacterController.
	/// side-effect: clears NoteCache.
	/// </summary>
	void EvaluateChord() {
		NoteCache.Sort ();
		if (NoteCache.Count == 1) {
			BasicMoveType moveType;
			int note = NoteCache[0];
			if (note == _LeftNote) {
				moveType = BasicMoveType.Left;
			}
			else if (note == _DownNote) {
				moveType = BasicMoveType.Down;
			}
			else if (note == _UpNote) {
				moveType = BasicMoveType.Up;
			}
			else if (note == _RightNote) {
				moveType = BasicMoveType.Right;
			}
			else {
				moveType = BasicMoveType.LightAttack;
			}
			BasicMove basicMove = new BasicMove(
				DateTime.Now,//ChordStart,
				note,
				1.0,
				moveType,
				InversionType.Standard
			);
		} else if (NoteCache.Count == 2) {
			BasicMoveType moveType;
			if (NoteCache[0] == _LeftNote && NoteCache[1] == _UpNote) {
				moveType = BasicMoveType.UpLeft;
			}
			else if (NoteCache[0] == _LeftNote && NoteCache[1] == _DownNote) {
				moveType = BasicMoveType.DownLeft;
			}
			else if (NoteCache[0] == _RightNote && NoteCache[1] == _UpNote) {
				moveType = BasicMoveType.UpRight;
			}
			else if (NoteCache[0] == _RightNote && NoteCache[1] == _DownNote) {
				moveType = BasicMoveType.DownRight;
			}
			else {
				moveType = BasicMoveType.LightAttack;
			}
		} else if (NoteCache.Count == 3) {
			double standardValue;
			int firstInterval = 0;


			double firstInversionValue;

			double secondInversionValue;
		}
	}
}
