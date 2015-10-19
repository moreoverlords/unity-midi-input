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
        currentAttack = null;
        fighterFSM = this.GetComponentInParent<PlayMakerFSM>();
	}
	
	// Update is called once per frame
	void Update () {
        attackKeysDown.Clear();
        // get state of keyboard
        for (int i = 0; i < 127; i++)
        {
            float key = FakeMidiInput.GetKey(i);
            if (key > 0)
            {
                attackKeysDown.Add(new Note
                {
                    Value = i,
                    FrameTimestamp = Time.frameCount
                });
            }
        }
        // if new attack, trigger matching event in fighterFSM
        if (attackKeysDown.Count > 0 && (currentAttack == null || !ListEquals<Note>(currentAttack, attackKeysDown)))
        {
            // read attacks
            switch (attackKeysDown.Count)
            {
                case 1:
                    fighterFSM.SendEvent("Light Attack");
                    break;
                case 2:
                    fighterFSM.SendEvent("Medium Attack");
                    break;
                case 3:
                    fighterFSM.SendEvent("Heavy Attack");
                    break;
                default:
                    break;
            }
            currentAttack = attackKeysDown;
        }
	}

    public void FinishCurrentAttack()
    {
        currentAttack = null;
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
