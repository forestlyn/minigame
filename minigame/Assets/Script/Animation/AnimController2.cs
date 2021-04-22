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
    }
}
