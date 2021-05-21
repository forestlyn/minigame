using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField] protected Database database;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float rayLength;
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
        if (database.isLying)
            return;

        if (rb.velocity.y < 0.05f)
        {
            database.jumping = false;
            //database.falling = true;
        }
        else
        {
            database.jumping = true;
        }

        if (rb.velocity.y < -0.05f)
        {
            database.falling = true;
        }
        else
        {
            database.falling = false;
        }

        bool isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayLength);
        Debug.DrawRay(transform.position, Vector2.down * rayLength,Color.red);
        if (isOnGround)
        {
            database.jumping = database.falling = false;
        }
    }
}
