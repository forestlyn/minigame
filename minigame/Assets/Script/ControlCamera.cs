using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    [Header("玩家")]
    [SerializeField] private Transform Players1;
    [SerializeField] private Transform Players2;
    [Tooltip("摄像机为初始尺寸时玩家间最大的距离")]
    [SerializeField] private float maxDistance;

    [Header("摄像机")]
    [Tooltip("摄像机")]
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [Tooltip("摄像机初始尺寸")]
    [SerializeField] private float defaultSize;
    [Tooltip("摄像机最大尺寸")]
    [SerializeField] private float maxSize;
    [Tooltip("摄像机当前大小")]
    [SerializeField] private float nowDistance;
    [Tooltip("每次摄像机增大或减小的大小")]
    [SerializeField] private float changeDistance;

    void Awake()
    {
        Players1 = GameObject.FindGameObjectWithTag("Pencil").transform;
        Players2 = GameObject.FindGameObjectWithTag("Eraser").transform;
        mainCamera = GetComponent<CinemachineVirtualCamera>();
        defaultSize = mainCamera.m_Lens.OrthographicSize;
    }

    void Update()
    {
        Position();
        View();
    }

    void Position()
    {
        //控制Camera移动,竖直方向一定距离内camera不移动
        float Distance = Players1.position.y - Players2.position.y;
        if (Distance < mainCamera.m_Lens.OrthographicSize / 2)
            transform.position = new Vector3(Players1.position.x + Players2.position.x, 0, 0) / 2 + new Vector3(0, 0, -10);
        else
            transform.position = (Players1.position + Players2.position) / 2 + new Vector3(0, 0, -10);
    }

    //视野缩放
    void View()
    {
        float distanceBetweenPlayers = (Players1.position - Players2.position).sqrMagnitude;
        //玩家距离过大
        if (distanceBetweenPlayers > maxDistance && distanceBetweenPlayers > nowDistance - changeDistance)
        {
            nowDistance = maxDistance + changeDistance;
            mainCamera.m_Lens.OrthographicSize = defaultSize / maxDistance * nowDistance;
            if (mainCamera.m_Lens.OrthographicSize > maxSize)
                mainCamera.m_Lens.OrthographicSize = maxSize;
        }
        else
        {
            mainCamera.m_Lens.OrthographicSize = defaultSize;
        }
    }
}