using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputParser : MonoBehaviour {

	public List<Note> NoteQueue;
	public int ChordTolerance;
	private int _FrameTimestamp;

	// Use this for initialization
	void Start () {
		NoteQueue = new List<Note> ();
		_FrameTimestamp = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// check keys for inputs
		for (var i = 0; i < 128; i++) {
			if (MidiInput.GetKeyDown (i)) {
				NoteQueue.Add (new Note {
					Value = i,
					FrameTimestamp = _FrameTimestamp
				});
			}
		}

		if (NoteQueue.Count > 0) {
			// Read all notes as a chord
		}
	}
}
