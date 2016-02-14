using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class HitstunBehaviour : StateMachineBehaviour {

    private float cachedSpeed;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //cachedSpeed = animator.speed;
        //animator.speed = 1 / animator.GetInteger("HitstunFrames");
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        int hitstunFrames = animator.GetInteger("HitstunFrames");
        if (hitstunFrames > 0)
        {
            animator.SetInteger("HitstunFrames", hitstunFrames - 1);
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //animator.speed = cachedSpeed;
        int comboCount = animator.GetInteger("ComboCount");
        Debug.Log(comboCount + "-hit combo performed!");
        animator.SetInteger("ComboCount", 0);
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
