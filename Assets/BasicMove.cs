using UnityEngine;
using System.Collections;

public class BasicMove {
	public int RootValue;
	public BasicMoveType MoveType;
	public double HarmonyValue; // 0-1
	public int FrameTimestamp;

	public BasicMove(int[] notes, int frameTimestamp) {
		FrameTimestamp = frameTimestamp;
		int noteCount = notes.Length;

	}
}

public enum BasicMoveType {
	DownBack = 1,
	Down = 2,
	DownForward = 3,
	Left = 4,
	Right = 6,
	UpBack = 7,
	Up = 8,
	UpForward = 9,
	LightPunch = 10,
	MediumPunch = 11,
	HeavyPunch = 12,
	LightKick = 13,
	MediumKick = 14,
	HeavyKick = 15
}
