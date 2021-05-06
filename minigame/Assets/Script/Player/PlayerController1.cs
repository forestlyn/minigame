using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : Player
{
    [Header("射线相关")]
    [Tooltip("检测前方是否有障碍物")]
    [SerializeField] private float pencilLength;
    [Tooltip("检测障碍物射线")]
    [SerializeField] GameObject[] rayPoints;

    [Header("铅笔相关")]
    [Tooltip("倒下和起来用时")]
    [SerializeField] public float downAndUpTime = 0.5f;
    [SerializeField] public float time = 10f;
    [SerializeField] private Animator anim;
    [Tooltip("检测楼梯射线长度")]
    [SerializeField] private float Stairs_Raylength;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (time == downAndUpTime)
        {
            HorizontalMove();
            Jump(IsOnPencilOrEraser(1));
        }
        
        //IsOnEraser();
        PencilDown();
        if (isLying) Down();
        else Up();
    }

    #region 铅笔倒下或站起
    private void PencilDown()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            if (isLying)
            {
                isLying = false;
                time = 0;
                anim.SetBool("lying", false);
                anim.SetBool("up", true);
            }
                
            else if (!isLying && !IsBlocked())
            {
                isLying = true;
                time = 0;
                anim.SetBool("lying", true);
            }
                
        }
        
    }
    //倒下时判断是否有障碍
    private bool IsBlocked()
    {
        bool isBlocked = false;
        for(int i = 0; i < rayPoints.Length; i++)
        {
            isBlocked = Physics2D.Raycast(rayPoints[i].transform.position, new Vector2(1 * faceDirection, 0f), pencilLength,1<<LayerMask.NameToLayer("Map"));
            if (isBlocked)
                return isBlocked;
        }
        for (int i = 0; i < rayPoints.Length; i++)
        {
            isBlocked = Physics2D.Raycast(rayPoints[i].transform.position, new Vector2(1 * faceDirection, 0f), pencilLength, 1 << LayerMask.NameToLayer("Eraser"));
            if (isBlocked)
                return isBlocked;
        }
        return isBlocked;
    }

    private void Up()
    {
        time += Time.deltaTime;
        if (time > downAndUpTime)
        {
            time = downAndUpTime;
        }
        if (faceDirection == 1)
            transform.eulerAngles = new Vector3(0, 0, -90 + 90 * faceDirection * time / downAndUpTime);
        else
            transform.eulerAngles = new Vector3(0, 180, -90 - 90 * faceDirection * time / downAndUpTime);
        return;
    }

    private void Down()
    {
        time += Time.deltaTime;
        if (time > downAndUpTime)
            time = downAndUpTime;
        if (faceDirection == 1)
            transform.eulerAngles = new Vector3(0, 0, -90 * faceDirection * time / downAndUpTime);
        else
            transform.eulerAngles = new Vector3(0, 180, 90 * faceDirection * time / downAndUpTime);
        return;
    }
    //铅笔站起后将状态切换为idle
    public void SetUpToFalse()
    {
        anim.SetBool("up", false);
    }
    #endregion
    //protected override void Jump(bool isOnPencilOrEraser)
    //{
    //    //Debug.Log(playerID + ":" + isOnPencilOrEraser);
    //    if (this.isLying)//倒下时禁止一切动作
    //        return;
    //    if (Stairs())
    //    {
    //        if (Input.GetButton("JumpPlayer" + playerID))
    //        {
    //            transform.position += new Vector3(0, speed, 0);
    //            return;
    //        }
                      
    //    }
    //    if (IsOnGround() || isOnPencilOrEraser)
    //    {
    //        if (Input.GetButton("JumpPlayer" + playerID))
    //        {
    //            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    //        }
    //    }
    //}
    //bool Stairs()
    //{
    //    return Physics2D.Raycast(transform.position, new Vector2(0, 1), Stairs_Raylength, 1 << LayerMask.NameToLayer("Stairs"));
    //}
}
