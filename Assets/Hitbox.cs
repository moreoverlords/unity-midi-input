using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour
{
    public string Name;

    /// <summary>
    /// Amount of damage done by the hitbox while it's active.
    /// Changed by AttackActiveBehaviour.
    /// </summary>
    public int damage;

    /// <summary>
    /// Number of frames of hitstun caused by this attack.
    /// Changed by AttackActiveBehaviour.
    /// </summary>
    public int hitstun;

    /// <summary>
    /// Number of frames of blockstun caused by this attack.
    /// Changed by AttackActiveBehaviour.
    /// </summary>
    public int blockstun;

    /// <summary>
    /// Whether or not the hitbox is currently active and should deal damage.
    /// </summary>
    public bool active;

    public int numHits;

    void Awake()
    {
        //coll = GetComponentInParent<Collider>();
    }

    // Use this for initialization
    void Start()
    {
        damage = 0;
        hitstun = 0;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hurtbox" && active && numHits > 0)
        {
            PlayerState opponentState = other.gameObject.GetComponentInParent<PlayerState>();
            opponentState.ReceiveHit(damage, hitstun, blockstun);
            numHits--;
        }
    }

    public void activate(int _damage, int _hitstun, int _blockstun, int _numHits)
    {
        active = true;
        damage = _damage;
        hitstun = _hitstun;
        blockstun = _blockstun;
        numHits = _numHits;
    }

    public void deactivate()
    {
        active = false;
        damage = 0;
        hitstun = 0;
        blockstun = 0;
        numHits = 0;
    }
}
