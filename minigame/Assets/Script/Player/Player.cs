using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("基本属性")]
    [Tooltip("移动速度")]
    [SerializeField] protected float speed;
    [Tooltip("跳跃速度（默认值）")]
    [SerializeField] protected float defaultJumpSpeed;
    [Tooltip("跳跃速度（真实值）")]
    [SerializeField] protected float jumpSpeed;
    [Tooltip("玩家序号  P1（铅笔）值为1  P2（橡皮）值为2")]
    [SerializeField] public int playerID;
    [Tooltip("铅笔是否躺着")]
    [SerializeField] public bool isLying;
    [Tooltip("面向方向（1为右 -1为左）")]
    [SerializeField] protected int faceDirection = 1;


    //[Header("Layer")]
    //[Tooltip("地面图层")]
    //[SerializeField] protected LayerMask ground;

    [Header("引用组件")]
    [Tooltip("玩家的刚体组件")]
    [SerializeField] protected Rigidbody2D rb;
    [Tooltip("玩家的transform组件")]
    [SerializeField]protected Transform player;
    //[SerializeField] protected Collider2D coll;

    private void Start()
    {
        jumpSpeed = defaultJumpSpeed;
    }

    //水平移动
    protected void HorizontalMove()
    {
        if (isLying)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
            
        float moveDirection = Input.GetAxisRaw("HorizontalPlayer" + playerID);
        switch (moveDirection)
        {
            case 1:
                rb.velocity = new Vector2(speed, rb.velocity.y);
                player.eulerAngles = new Vector2(0, 0);
                faceDirection = 1;
                break;
            case -1:
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                player.eulerAngles = new Vector2(0, 180);
                faceDirection = -1;
                break;
            default:
                rb.velocity = new Vector2(0f, rb.velocity.y);
                break;
        }
    }

    //跳跃
    protected void Jump(bool isLying)
    {
        if (isLying)
            return;
        //判断角色接触地面(在斜坡上运动时会有问题)
        if (Input.GetButton("JumpPlayer" + playerID) && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
