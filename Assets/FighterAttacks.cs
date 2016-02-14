using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterAttacks : MonoBehaviour {

    public float maxAttackHeight = 1f;
    public float minAttackHeight = 0f;
    public float punchKickBorder = .5f;
    public Dictionary<AttackType, float> attackRange;
    public Dictionary<AttackType, FrameData> frameData;
    public int movementFrames;
    float attackHeightInterval;
    public AnimationClip attackAnimation;
    public AnimationClip movementAnimation;
    private const double secondsPerFrame = 1f / 60f;
    private PlayMakerFSM fighterFSM;
    public Attack CurrentAttack;

    // Use this for initialization
    void Start() {
        fighterFSM = this.GetComponentInParent<PlayMakerFSM>();
        attackHeightInterval = (maxAttackHeight - minAttackHeight) / 12;
        frameData = new Dictionary<AttackType, FrameData>();
        attackRange = new Dictionary<AttackType, float>();

        frameData[AttackType.Light] = new FrameData
        {
            startup = 2,
            active = 5,
            recovery = 4
        };
        attackRange[AttackType.Light] = 2f;

        frameData[AttackType.Medium] = new FrameData
        {
            startup = 3,
            active = 7,
            recovery = 6
        };
        attackRange[AttackType.Medium] = 3f;

        frameData[AttackType.Heavy] = new FrameData
        {
            startup = 4,
            active = 10,
            recovery = 8
        };
        attackRange[AttackType.Heavy] = 4f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// triggers an attack of the specified parameters
    /// </summary>
    /// <param name="rootNote">base note of the chord (0-11, modulo'd). Determines attack height</param>
    /// <param name="harmonyValue">numeric value of the harmony of the chord (0-1)</param>
    /// <param name="movement">whether to move forward, back, or stay still as attack executes</param>
    /// <param name="attackType">strength of the attack</param>
    public void PerformAttack(Attack attack)
    {

        FrameData attackFrameData;
        if (!frameData.TryGetValue(attack.attackType, out attackFrameData))
        {
            print("attack " + attack.attackType + " not defined for this character");
            return;
        }
        float attackHeight = minAttackHeight + attackHeightInterval * attack.rootNote;
        bool isKick = (attackHeight < punchKickBorder);

        float startX = 0f;//transform.Find("Body").position.x;
        //float startY = 0f;

        /*if (isKick)
        {
            startY = transform.Find("Body").position.y - 0.5f;
        }
        else //is punch
        {
            startY = transform.Find("Body").position.y;
        }*/

        float now = Time.time;
        float startupEnd = now + (float)(attackFrameData.startup * secondsPerFrame);
        float activeEnd = startupEnd + (float)(attackFrameData.active * secondsPerFrame);
        float recoveryEnd = activeEnd + (float)(attackFrameData.recovery * secondsPerFrame);

        AnimationCurve xCurve = new AnimationCurve();
        xCurve.AddKey(startupEnd, startX);
        xCurve.AddKey(activeEnd, startX + attackRange[attack.attackType]);
        xCurve.AddKey(recoveryEnd, startX);

        /*AnimationCurve yCurve = new AnimationCurve();
        yCurve.AddKey(startupEnd, startY);
        yCurve.AddKey(activeEnd, attackHeight);
        yCurve.AddKey(recoveryEnd, startY);*/

        string targetPath;
        if (isKick)
        {
            targetPath = "LeftFoot";
        }
        else // is punch
        {
            targetPath = "LeftHand";
        }

        attackAnimation.SetCurve(targetPath, typeof(Transform), "localPosition.x", xCurve);
        //attackAnimation.SetCurve(targetPath, typeof(Transform), "localPosition.x", yCurve);

        switch (attack.attackType)
        {
            case AttackType.Light:
                fighterFSM.SendEvent("LightAttack");
                break;
            case AttackType.Medium:
                fighterFSM.SendEvent("MediumAttack");
                break;
            case AttackType.Heavy:
                fighterFSM.SendEvent("HeavyAttack");
                break;
        }
    }

    public void Move(MovementType movement, AttackType attackType)
    {

    }

    public void SpecialAttack()
    {

    }
}

public enum AttackType
{
    Light,
    Medium,
    Heavy
}

public enum MovementType
{
    Back,
    Neutral,
    Forward
}

public struct FrameData
{
    public int startup;
    public int active;
    public int recovery;
}

public struct Attack
{
    public int rootNote;
    public float harmonyValue;
    public MovementType movement;
    public AttackType attackType;
}