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
        AnimSwitchForJumpAndFall(2);

        anim.SetBool("running", database.running);
        anim.SetBool("jumping", database.jumping);
        anim.SetBool("falling", database.falling);
        anim.SetBool("accumulating", database.accumulate);
        anim.SetBool("wiping", database.isWiping);
        if (database.falling)
        {
            if (Physics2D.Raycast(transform.position, new Vector2(0, -1), rayLength1, 1 << LayerMask.NameToLayer("Map")))
            {
                fall.Play();
            }
        }
        Debug.DrawRay(transform.position, new Vector2(0, -1) * rayLength1, Color.black);
    }
    
}
