using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : Player
{
    [Header("角色属性")]
    [Tooltip("最大蓄力时间")]
    [SerializeField] private float maxAccumulateTime = 1;
    [Tooltip("蓄力时间")]
    [SerializeField] private float accumulateTime = 0;
    [Tooltip("角色蓄力跳最大高度与普通跳跃高度之比")]
    [SerializeField] private float ratio = 1.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        //transform.position = playerOriginPosition.position;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        HorizontalMove();
        Jump(IsOnPencilOrEraser(2));
        AccumulateJump();
    }

    //蓄力跳跃
    protected void AccumulateJump()
    {
        if (playerID == 2 && !otherDatabase.isLying && IsOnPencilOrEraser(2))//橡皮，铅笔直立，站在铅笔上
        {
            return;
        }
            

        if (rb.velocity.y < 0 && !Physics2D.Raycast(transform.position,Vector3.down,jumpRayLength,1<<LayerMask.NameToLayer("Elevator")))//速度小于0
        {
            database.accumulate = false;
            return;
        }



        if (!database.jumping && !database.falling)
        {
            //Debug.DrawRay(transform.position, Vector3.down * jumpRayLength, Color.green);
            //蓄力
            if (Input.GetButton("Accumulate"))
            {
                database.accumulate = true;
                database.running = false;
                if (accumulateTime < 1f)
                    accumulateTime += Time.deltaTime;
            }
            //跳跃
            if (Input.GetButtonUp("Accumulate") && (Mathf.Abs(rb.velocity.y) < 0.05f || Physics2D.Raycast(transform.position, Vector3.down, jumpRayLength, 1 << LayerMask.NameToLayer("Elevator"))))
            {
                jump.Play();
                database.accumulate = false;
                database.jumping = true;
                float _ratio = (Mathf.Sqrt(ratio) - 0.8f) * accumulateTime / maxAccumulateTime;
                rb.velocity = new Vector2(rb.velocity.x, database.jumpSpeed * (0.8f + _ratio));
                accumulateTime = 0;
            }
        }
    }
}
