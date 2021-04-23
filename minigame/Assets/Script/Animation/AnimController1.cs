using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController1 : Anim
{
    private PlayerController1 playerController1;//铅笔脚本，用于获取其isLying值
    private void Start()
    {
        playerController1 = GetComponent<PlayerController1>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        isLying = playerController1.isLying;

        AnimSwitchForRun();

        AnimSwitchForJumpAndFall(1);
    }
    //??
    void AnimSwitchForLie()
    {
        if (Input.GetKey(KeyCode.S))
        {

        }
    }
}
