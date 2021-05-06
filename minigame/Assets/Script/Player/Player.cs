using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("基本属性")]
    [Tooltip("玩家序号  P1（铅笔）值为1  P2（橡皮）值为2")]
    [SerializeField] public int playerID;
    [Space]
    //[Tooltip("移动速度")]
    //[SerializeField] protected float speed;
    //[Tooltip("跳跃速度（默认值）")]
    //[SerializeField] protected float defaultJumpSpeed;
    //[Tooltip("跳跃速度（真实值）")]
    //[SerializeField] protected float jumpSpeed;
    [Space]
    [Tooltip("面向方向（1为右 -1为左）")]
    [SerializeField] protected int faceDirection = 1;

    [Header("射线")]
    [Tooltip("用于检测是否在地面上")]
    [SerializeField] private float jumpRayLength = 0.5f;
    [Tooltip("跳跃判定点")]
    [SerializeField] private List<Transform> jumpPoint;
    [Space]
    [Tooltip("用于检测脚下是否有铅笔或橡皮")]
    [SerializeField] private float rayLength;

    [Header("引用组件")]
    [Tooltip("玩家的刚体组件")]
    [SerializeField] protected Rigidbody2D rb;
    [Tooltip("玩家的transform组件")]
    [SerializeField] protected Transform player;
    [Space]
    [Tooltip("角色状态")]
    [SerializeField] protected Database database;
    private void Start()
    {
        //database.speed = 6;
        database.jumpSpeed = database.defaultJumpSpeed;
    }

    //水平移动
    protected void HorizontalMove()
    {
        if (database.isLying)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        float moveDirection = Input.GetAxisRaw("HorizontalPlayer" + playerID);
        switch (moveDirection)
        {
            case 1:
                rb.velocity = new Vector2(database.speed, rb.velocity.y);
                player.eulerAngles = new Vector2(0, 0);
                faceDirection = 1;
                database.idle = false;
                database.running = true;
                break;
            case -1:
                rb.velocity = new Vector2(-database.speed, rb.velocity.y);
                player.eulerAngles = new Vector2(0, 180);
                faceDirection = -1;
                database.idle = false;
                database.running = true;
                break;
            default:
                rb.velocity = new Vector2(0f, rb.velocity.y);
                database.running = false;
                break;
        }
    }

    #region 跳跃
    protected void Jump(bool isOnPencilOrEraser)
    {
        //Debug.Log(playerID + ":" + isOnPencilOrEraser);
        if (database.isLying)//倒下时禁止一切动作
            return;

        if (IsOnGround() || isOnPencilOrEraser && !database.jumping)
        {
            if (Input.GetButton("JumpPlayer" + playerID))
            {
                rb.velocity = new Vector2(rb.velocity.x, database.jumpSpeed);
                database.running = false;
                database.idle = false;
                database.jumping = true;
            }
        }
    }

    protected bool IsOnGround()
    {
        bool isOnGround = false;
        Vector2 downDirection = new Vector2(0, -1);
        foreach (Transform item in jumpPoint)
        {
            isOnGround = Physics2D.Raycast(item.position, downDirection, jumpRayLength, 1 << LayerMask.NameToLayer("Map"));
            Debug.DrawRay(item.position, downDirection * jumpRayLength, Color.green);
        }
        //Debug.Log("isOnGround = " + isOnGround);
        return isOnGround;
    }

    protected bool IsOnPencilOrEraser(int playerID)
    {//铅笔playerID为1 橡皮playerID为2 
        if (playerID != 1 && playerID != 2)
        {
            Debug.Log("Player.cs :1");
        }


        Vector2 downDirection = new Vector2(0, -1);
        bool isOnPencilOrEraser;
        if (playerID == 1)
        {
            isOnPencilOrEraser = Physics2D.Raycast(transform.position, downDirection, rayLength, 1 << LayerMask.NameToLayer("Eraser"));

            if (isOnPencilOrEraser)
                database.jumpSpeed = database.defaultJumpSpeed * 1.22f;
            else
                database.jumpSpeed = database.defaultJumpSpeed;
        }
        else
        {
            isOnPencilOrEraser = Physics2D.Raycast(transform.position, downDirection, rayLength, 1 << LayerMask.NameToLayer("Pencil"));
        }
        Debug.DrawRay(transform.position, downDirection * rayLength, Color.black);
        return isOnPencilOrEraser;
    }
    #endregion
}
   

