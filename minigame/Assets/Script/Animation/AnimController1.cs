using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController1 : Anim
{
    private PlayerController1 playerController1;//铅笔脚本，用于获取其isLying值
    private void Start()
    {
        playerController1 = GetComponent<PlayerController1>();//
        rb = GetComponent<Rigidbody2D>();//

        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerController1.time == playerController1.downAndUpTime)
        {
            AnimSwitchForJumpAndFall(1);
        }
        anim.SetBool("running", database.running);
        anim.SetBool("jumping", database.jumping);
        anim.SetBool("falling", database.falling);
        anim.SetBool("lying", database.isLying);
        anim.SetBool("up", database.up);
    }
    
}
