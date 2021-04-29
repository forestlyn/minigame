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
        isLying = false;
    }

    void Update()
    {
        HorizontalMove();
        Jump(isLying, false);
        AccumulateJump();
    }

    //蓄力跳跃
    protected void AccumulateJump()
    {
        //蓄力
        if (Input.GetButton("Accumulate"))
        {
            if (accumulateTime < 1f)
                accumulateTime += Time.deltaTime;
        }
        //跳跃
        if (Input.GetButtonUp("Accumulate") && Mathf.Abs(rb.velocity.y)<0.05f)
        {
            float _ratio = (Mathf.Sqrt(ratio) - 0.5f) * accumulateTime / maxAccumulateTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * (0.5f + _ratio));
            accumulateTime = 0;
        }
    }

}
