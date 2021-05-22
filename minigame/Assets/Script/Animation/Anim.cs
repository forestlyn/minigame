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

        if (rb.velocity.y < -0.0001f)
        {
            database.jumping = false;
            database.falling = true;
        }
        else if (rb.velocity.y > 0.0001f)
        {
            database.falling = false;
            database.jumping = true;
        }
        else
        {
            database.jumping = database.falling = false; 
        }
        

        bool isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayLength,1<<LayerMask.NameToLayer("Map"));
        if (!isOnGround)
        {
            isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayLength, 1 << LayerMask.NameToLayer("Elevator"));
        }
        
        if(!isOnGround && playerID == 1)
        {
            isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayLength, 1 << LayerMask.NameToLayer("Eraser"));
        }
        else if(!isOnGround && playerID == 2)
        {
            isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayLength, 1 << LayerMask.NameToLayer("Pencil"));
        }
         
        Debug.DrawRay(transform.position + new Vector3(1,0,0), Vector2.down * rayLength,Color.red);
        if (isOnGround)
        {
            database.jumping = database.falling = false;
        }
    }
}
