using UnityEngine;
using System;
using System.Collections;

public class BasicMove {
	public int RootValue;
	public BasicMoveType MoveType;
	public double HarmonyValue; // 0-1
	public DateTime Timestamp;
	public InversionType Inversion;

	public BasicMove(DateTime timestamp, int rootValue, double harmonyValue, BasicMoveType moveType, InversionType inversion) {
		Timestamp = timestamp;
		RootValue = rootValue;
		HarmonyValue = harmonyValue;
		MoveType = moveType;
		Inversion = inversion;
	}
}

public enum BasicMoveType {
	DownLeft = 1,
	Down = 2,
	DownRight = 3,
	Left = 4,
	Neutral = 5,
	Right = 6,
	UpLeft = 7,
	Up = 8,
	UpRight = 9,
	LightAttack = 10,
	MediumAttack = 11,
	HeavyAttack = 12
}

public enum InversionType {
	Standard = 0,
	First = 1,
	Second = 2
}
