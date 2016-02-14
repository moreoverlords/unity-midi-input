using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

    /// <summary>
    /// The player's current health.
    /// </summary>
    public int health;

    /// <summary>
    /// Whether or not the character is currently vulnerable to being hit.
    /// i.e. probably not vulnerable if on the ground, etc.
    /// </summary>
    public bool vulnerable;

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = transform.GetComponent<Animator>();
	}
	
    public void ReceiveHit(int damage, int hitstun, int blockstun)
    {
        if (vulnerable)
        {
            if (false) // blocking
            {

            }
            else
            {
                health = health - damage;
                if (health < 0)
                {
                    health = 0;
                }
                Debug.Log("Damage taken: " + damage);
                anim.SetInteger("HitstunFrames", hitstun);
                int comboCount = anim.GetInteger("ComboCount");
                anim.SetInteger("ComboCount", comboCount + 1);
            }
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            // KO
        }
    }
}
