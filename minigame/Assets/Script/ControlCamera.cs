using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ControlCamera : MonoBehaviour
{
    [Header("玩家")]
    [SerializeField] private Transform Players1;
    [SerializeField] private Transform Players2;
    [Tooltip("摄像机为初始尺寸时玩家间最大的距离")]
    [SerializeField] private float maxDistancex;
    [SerializeField] private float maxDistancey;

    [Header("摄像机")]
    [Tooltip("摄像机")]
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [Tooltip("摄像机初始尺寸")]
    [SerializeField] private float defaultSize;
    [Tooltip("摄像机最大尺寸")]
    [SerializeField] private float maxSize;
    [Tooltip("相机移动速度")]

    [SerializeField] private float speed;
    [Tooltip("相机大小改变速率")]
    [SerializeField] private float rate;
    //[Tooltip("每次摄像机增大或减小的大小")]
    //[SerializeField] private float changeDistance;
    private float aimSize;//相机目标尺寸



    [SerializeField] private Transform Target;
    void Awake()
    {
        Players1 = GameObject.FindGameObjectWithTag("Pencil").transform;
        Players2 = GameObject.FindGameObjectWithTag("Eraser").transform;
        mainCamera = GetComponent<CinemachineVirtualCamera>();
        defaultSize = mainCamera.m_Lens.OrthographicSize;
    }
    private void Start()
    {
        transform.position = (Players1.position + Players2.position) / 2 + new Vector3(0, 0, -10);
    }
    void Update()
    {
        //transform.position = Target.position;
        Follow();
        View();
    }

    //视野缩放
    void View()
    {
        //玩家间距离
        float xdistanceBetweenPlayers = Mathf.Abs(Players1.position.x - Players2.position.x);
        float ydistanceBetweenPlayers = Mathf.Abs(Players1.position.y - Players2.position.y);
        //玩家距离过大
        if (xdistanceBetweenPlayers > maxDistancex||ydistanceBetweenPlayers>maxDistancey)
        {
            aimSize = Mathf.Min(Mathf.Max(defaultSize / maxDistancex * xdistanceBetweenPlayers, defaultSize / maxDistancey * ydistanceBetweenPlayers), maxSize);
            //mainCamera.m_Lens.OrthographicSize = Mathf.Min(Mathf.Max(defaultSize / maxDistancex * xdistanceBetweenPlayers, defaultSize / maxDistancey * ydistanceBetweenPlayers), maxSize);
        }
        else
        {
            aimSize = defaultSize;
            //mainCamera.m_Lens.OrthographicSize = defaultSize;
        }

        Zoom();
    }

    private void Zoom()
    {
        if (mainCamera.m_Lens.OrthographicSize > aimSize)//缩小
        {
            float change = rate * Time.deltaTime;
            if (mainCamera.m_Lens.OrthographicSize > change + aimSize)
                mainCamera.m_Lens.OrthographicSize -= change;
            else
                mainCamera.m_Lens.OrthographicSize = aimSize;
        }
        else if(mainCamera.m_Lens.OrthographicSize < aimSize)//放大
        {
            float change = rate * Time.deltaTime;
            if (mainCamera.m_Lens.OrthographicSize + change < aimSize)
                mainCamera.m_Lens.OrthographicSize += change;
            else
                mainCamera.m_Lens.OrthographicSize = aimSize;
        }
    }

    void Follow()
    {
        //transform.position = (Players1.position + Players2.position) / 2 + new Vector3(0, 0, -10);        
        Vector3 dir = ((Players1.position + Players2.position) / 2 - transform.position - new Vector3(0,0,10));
        transform.position = transform.position + dir * (speed * Time.fixedDeltaTime);
    }
}