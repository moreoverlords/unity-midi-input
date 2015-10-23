using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KeyboardStateParser : MonoBehaviour {
    private List<Note> moveKeysDown;
    private List<Note> attackKeysDown;

    public List<Note> currentAttack;

    PlayMakerFSM fighterFSM;

	// Use this for initialization
	void Start () {
        attackKeysDown = new List<Note>();
        lastFrameNotes = new List<Note>();
        currentAttack = null;
        fighterFSM = this.GetComponentInParent<PlayMakerFSM>();
	}

    private List<Note> lastFrameNotes;

    // Update is called once per frame
    void Update() {
        attackKeysDown.Clear();
        // get state of keyboard
        for (int i = 0; i < 127; i++)
        {
            float key = FakeMidiInput.GetKey(i);
            if (key > 0)
            {
                attackKeysDown.Add(new Note
                {
                    Value = i
                });
            }
        }
        // if new attack, trigger matching event in fighterFSM
        /*bool attackIsNew = true;
        if (currentAttack != null && currentAttack.Count > 0 && attackKeysDown.Count > 0) {
            attackIsNew = !NoteListEquals(currentAttack, attackKeysDown);
        }*/
        if (attackKeysDown.Count > 0)
        {
            if (!NoteListEquals(attackKeysDown, lastFrameNotes))
            {
                // read attacks
                switch (attackKeysDown.Count)
                {
                    case 1:
                        fighterFSM.SendEvent("LightAttack");
                        break;
                    case 2:
                        fighterFSM.SendEvent("MediumAttack");
                        break;
                    case 3:
                        fighterFSM.SendEvent("HeavyAttack");
                        break;
                    default:
                        break;
                }
                currentAttack = attackKeysDown;
            }
        }
        lastFrameNotes = attackKeysDown.ToList();
	}

    public void FinishCurrentAttack()
    {
        currentAttack = null;
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

    public static bool ListEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
    {
        var cnt = new Dictionary<T, int>();
        foreach (T s in list1)
        {
            if (cnt.ContainsKey(s))
            {
                cnt[s]++;
            }
            else
            {
                cnt.Add(s, 1);
            }
        }
        foreach (T s in list2)
        {
            if (cnt.ContainsKey(s))
            {
                cnt[s]--;
            }
            else
            {
                return false;
            }
        }
        return cnt.Values.All(c => c == 0);
    }
}
