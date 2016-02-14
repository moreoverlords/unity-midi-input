using UnityEngine;
using System.Collections;
using System.Linq;

public class AttackActiveBehaviour : StateMachineBehaviour {

    public string Name;

    private Hitbox hitbox;

    /// <summary>
    /// Amount of damage done by this attack.
    /// </summary>
    public int damage;
    
    /// <summary>
    /// Number of frames of hitstun caused by this attack.
    /// </summary>
    public int hitstun;

    /// <summary>
    /// Number of frames of blockstun caused by this attack.
    /// </summary>
    public int blockstun;

    /// <summary>
    /// Number of hits caused by this attack.
    /// </summary>
    public int numHits = 1;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitbox = animator.GetComponentsInChildren<Hitbox>().Where(hb => hb.Name == Name).FirstOrDefault();
        hitbox.activate(damage, hitstun, blockstun, numHits);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        hitbox.deactivate();
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
