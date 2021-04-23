using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : Player
{
    [Header("射线相关")]
    //[Space(10)]
    [Tooltip("检测脚下是否有橡皮的射线长度")]
    [SerializeField] private float rayLength;
    [Tooltip("检测前方是否有障碍物")]
    [SerializeField] private float pencilLength;
    [Tooltip("射线")]
    [SerializeField] GameObject[] rayPoints;

    [Header("铅笔相关")]
    [SerializeField] float downAndUpTime = 0.25f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
    }

    private void Update()
    {
        HorizontalMove();
        Jump(isLying);
        IsOnEraser();
        PencilDown();
    }
    //判断是否在橡皮上
    bool IsOnEraser()
    {
        Vector2 downDirection = new Vector2(0, -1);
        bool isOnEraser = Physics2D.Raycast(transform.position, downDirection, rayLength,1<<LayerMask.NameToLayer("Eraser"));
        if (isOnEraser)
        {
            jumpSpeed = defaultJumpSpeed * 1.22f;
        }
        else
        {
            jumpSpeed = defaultJumpSpeed;
        }
        return isOnEraser;
    }
    //铅笔倒下或站起
    private void PencilDown()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isLying)
                Down();
            else if(!isLying)
                Up();
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

    private void Down()
    {
        if (faceDirection == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
        isLying = false;
        return;
    }

    private void Up()
    {
        if (IsBlocked())
            return;
        if (faceDirection == 1)
            transform.eulerAngles = new Vector3(0, 0, -90 * faceDirection);
        else
            transform.eulerAngles = new Vector3(0, 180, 90 * faceDirection);
        //transform.position += new Vector3(0.55f, 0, 0);
        isLying = true;
        return;
    }
}
