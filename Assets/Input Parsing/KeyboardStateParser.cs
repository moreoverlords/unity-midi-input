using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KeyboardStateParser : MonoBehaviour {
    private List<Note> moveBlockKeysDown;
    private List<Note> attackKeysDown;
    private List<Note> lastFrameNotes;

    private Queue<BufferedInput> inputBuffer;

    public int inputBufferLength = 3;

    public int leftMovementStart = 0;
    public int rightMovementStart = 12;

    public int highRangeStart = 68;
    public int midRangeStart = 64;
    public int lowRangeStart = 60;

    Animator anim;
    FighterAttacks fighterScript;

	// Use this for initialization
	void Start () {
        attackKeysDown = new List<Note>();
        moveBlockKeysDown = new List<Note>();
        lastFrameNotes = new List<Note>();
        anim = this.GetComponentInParent<Animator> ();
        fighterScript = this.GetComponentInParent<FighterAttacks>();
        inputBuffer = new Queue<BufferedInput>();
    }

    // Update is called once per frame
    void Update() {
        attackKeysDown.Clear();
        moveBlockKeysDown.Clear();
        // get state of keyboard
        for (int i = 0; i < 127; i++)
        {
            float key = FakeMidiInput.GetKey(i);
            if (key > 0)
            {
                if (i >= lowRangeStart)
                {
                    attackKeysDown.Add(new Note
                    {
                        Value = i,
                        FrameTimestamp = Time.frameCount
                    });
                }
                else
                {
                    moveBlockKeysDown.Add(new Note
                    {
                        Value = i,
                        FrameTimestamp = Time.frameCount
                    });
                }
            }
        }
        if (FakeMidiInput.GetKey(0) > 0)
        {
            if (!anim.GetBool("MovingLeft"))
            {
                anim.SetBool("MovingLeft", true);
                Debug.Log("Moving left");
            }
        }
        else
        {
            if (anim.GetBool("MovingLeft"))
            {
                anim.SetBool("MovingLeft", false);
                Debug.Log("Stopped moving left");
            }
        }
        if (FakeMidiInput.GetKey(1) > 0)
        {
            if (!anim.GetBool("MovingRight"))
            {
                anim.SetBool("MovingRight", true);
                Debug.Log("Moving right");
            }
        }
        else
        {
            if (anim.GetBool("MovingRight"))
            {
                anim.SetBool("MovingRight", false);
                Debug.Log("Stopped moving right");
            }
        }
        if (attackKeysDown.Count > 0 && !NoteListEquals(attackKeysDown, lastFrameNotes))
        {
            int rootNote = attackKeysDown[0].Value;

            // TODO: process movement and blocking
            // idea:   lowest octave moves left, second lowest moves right
            //          single notes just move, two notes dash, three notes block
            //              blocking: major chord --> block high, minor chord --> block low
            //              low block stops low and mid, high block stops overhead and mid

            // process attack height
            if (rootNote >= highRangeStart)
            {
                //inputBuffer.Enqueue(new BufferedInput("Overhead"));
                anim.SetInteger("AttackHeight", (int)AttackHeight.Overhead);
            }
            else if (rootNote >= midRangeStart)
            {
                //inputBuffer.Enqueue(new BufferedInput("Mid"));
                anim.SetInteger("AttackHeight", (int)AttackHeight.Mid);
            }
            else if (rootNote >= lowRangeStart)
            {
                //inputBuffer.Enqueue(new BufferedInput("Low"));
                anim.SetInteger("AttackHeight", (int)AttackHeight.Low);
            }

            // read attacks
            switch (attackKeysDown.Count)
            {
                case 1:
                    //Debug.Log("LightAttack enqueued"); 
                    inputBuffer.Enqueue(new BufferedInput("LightAttack"));
                    break;
                case 2:
                    //Debug.Log("MediumAttack enqueued");
                    inputBuffer.Enqueue(new BufferedInput("MediumAttack"));
                    break;
                case 3:
                default:
                    //Debug.Log("HeavyAttack enqueued");
                    inputBuffer.Enqueue(new BufferedInput("HeavyAttack"));
                    break;
            }
        }
        lastFrameNotes = attackKeysDown.ToList();

        // clear old inputs from buffer
        if (inputBuffer.Count() > 0)
        {
            while (inputBuffer.Count() > 0 && inputBuffer.Peek().framesInBuffer >= inputBufferLength)
            {
                //Debug.Log("frames in buffer: " + inputBuffer.Peek().framesInBuffer);
                anim.ResetTrigger(inputBuffer.Peek().triggerName);
                //Debug.Log(inputBuffer.Peek().triggerName + " dequeued");
                inputBuffer.Dequeue();
            }

            // set all buffered triggers and increment frame counts
            foreach (BufferedInput input in inputBuffer)
            {
                anim.SetTrigger(input.triggerName);
                input.framesInBuffer++;
            }
        }
	}

    private bool NoteListEquals(List<Note> list1, List<Note> list2)
    {
        List<int> list1Values = list1.Select(note => note.Value).ToList();
        List<int> list2Values = list2.Select(note => note.Value).ToList();
        foreach (int note in list1Values)
        {
            if (!list2Values.Contains(note))
            {
                return false;
            }
        }
        return true;
    }

    public enum AttackHeight
    {
        Low,
        Mid,
        Overhead
    }

    public class BufferedInput
    {
        public int framesInBuffer;
        public string triggerName;

        public BufferedInput(string name)
        {
            framesInBuffer = 0;
            triggerName = name;
        }
    };
}
