using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform targetPos;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        boss = animator.GetComponent<Boss>();
        rb = animator.GetComponent<Rigidbody2D>();
        targetPos = boss.NewTargetPos();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float speed = boss.speed;
        boss.LookAtPlayer();

        Vector2 pos = Vector2.MoveTowards(rb.position,targetPos.position,speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);

        if(Vector2.Distance(rb.position,targetPos.position)<=0.1f)
        {
            int randomAttack = Random.Range(0,3);
            if(randomAttack ==0)
            {
                animator.SetTrigger("attack1");
            }
            else if(randomAttack ==1)
            {
                animator.SetTrigger("attack2");
            }
            else
            {
                animator.SetTrigger("attack3");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");
    }

}
