using UnityEngine;
using System.Collections;
using System;

public class Note {

	/// <summary>
	/// Value of the note. 0-128
	/// </summary>
	public int Value;

	/// <summary>
	/// Frame in which the note was hit (since start of round).
	/// </summary>
	public DateTime TimeStamp;

}
