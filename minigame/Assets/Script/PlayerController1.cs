using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : Player
{
    [Space(20)]
    [Tooltip("检测脚下是否有橡皮的射线长度")]
    [SerializeField] private float rayLength;
    [Tooltip("检测前方是否有障碍物")]
    [SerializeField] private float pencilLength;

    [Space(20)]
    [Tooltip("射线")]
    [SerializeField] GameObject[] rayPoints;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMove();
        Jump(isLying);
        IsOnEraser();
        PencilDown();
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

    private void PencilDown()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (IsBlocked())
                return;
            transform.eulerAngles = new Vector3(0, 0, -90);
            isLying = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position += new Vector3(0, 0.5f, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
            isLying = false;
        }
    }

    private bool IsBlocked()
    {
        bool isBlocked = false;
        for(int i = 0; i < rayPoints.Length; i++)
        {
            isBlocked = Physics2D.Raycast(rayPoints[i].transform.position, new Vector2(1 * faceDirection, 0f), pencilLength,1<<LayerMask.NameToLayer("Map"));
            if (isBlocked)
                break;
        }
        return isBlocked;
    }
}
