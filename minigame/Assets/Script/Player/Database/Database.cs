using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    [Header("角色状态")]
    public bool idle;
    public bool running;
    public bool jumping;
    public bool falling;
    [Header("铅笔")]
    public bool isLying;
    public bool up;
    [Header("橡皮")]
    public bool accumulate;

    [Header("角色参数")]
    [SerializeField] public float speed = 6;
    [SerializeField] public float defaultJumpSpeed = 20;
    [SerializeField] public float jumpSpeed;

}
