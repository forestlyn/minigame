using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;

    protected void AnimSwitchForRun()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    protected void AnimSwitchForJumpAndFall(int playerID)
    {
        //当按下跳跃键时
        if (Input.GetButtonDown("JumpPlayer" + playerID) && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            anim.SetBool("jumping", true);
        }
        //当跳跃过程中 Y轴速度小于0
        else if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y <= 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            anim.SetBool("falling", false);
        }
    }
}
