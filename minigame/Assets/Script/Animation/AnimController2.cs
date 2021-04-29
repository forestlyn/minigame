using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController2 : Anim
{
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimSwitchForRun();

        AnimSwitchForJumpAndFall(2);
        AccumulateJump();
    }

    protected void AccumulateJump()
    {
        //蓄力
        if (Input.GetButton("Accumulate"))
        {
            anim.SetBool("accumulating", true);
        }
        //跳跃
        if (Input.GetButtonUp("Accumulate") && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            anim.SetBool("accumulating", false);
            anim.SetBool("jumping", true);
        }
    }
}
