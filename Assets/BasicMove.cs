using UnityEngine;
using System;
using System.Collections;

public class BasicMove {
	public int RootValue;
	public BasicMoveType MoveType;
	public double HarmonyValue; // 0-1
	public DateTime Timestamp;

	public BasicMove(int timestamp, int rootValue, double harmonyValue, BasicMoveType moveType) {
		Timestamp = timestamp;
		RootValue = rootValue;
		HarmonyValue = harmonyValue;
		MoveType = moveType;
	}
}

public enum BasicMoveType {
	DownBack = 1,
	Down = 2,
	DownForward = 3,
	Left = 4,
	Neutral = 5,
	Right = 6,
	UpBack = 7,
	Up = 8,
	UpForward = 9,
	LightAttack = 10,
	MediumAttack = 11,
	HeavyAttack = 12
}
