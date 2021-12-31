﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleTransition : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerController.Instance.IsSelfHeal)
            PlayerController.Instance.IsSelfHeal = false;

        if(PlayerController.Instance.Rigidbody.constraints != RigidbodyConstraints2D.FreezeRotation)
            PlayerController.Instance.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (PlayerController.Instance.IsTimeComboAttack && !PlayerData.IsWaterSpearUsed())
        {
            animator.Play("player attack anim");
        }
        if (PlayerController.Instance.IsTimeComboAttack && PlayerData.IsWaterSpearUsed())
        {
            animator.Play("player water spear 1 anim");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Instance.IsTimeComboAttack = false;

        if (PlayerController.Instance.IsSelfHeal)
            PlayerController.Instance.IsSelfHeal = false;

        if (PlayerController.Instance.Rigidbody.constraints != RigidbodyConstraints2D.FreezeRotation)
            PlayerController.Instance.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
