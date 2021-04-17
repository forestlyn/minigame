using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : Player
{
    [Space(20)]
    [Tooltip("检测脚下是否有橡皮的射线长度")]
    [SerializeField] private float rayLength;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMove();
        Jump();
        IsOnEraser();
    }

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
}
