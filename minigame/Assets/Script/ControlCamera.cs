using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    [Header("玩家")]
    [SerializeField] private Transform Players1;
    [SerializeField] private Transform Players2;
    [SerializeField] private float maxDistance;
    [Header("摄像机")]
    [Tooltip("摄像机")]
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [Tooltip("摄像机初始尺寸")]
    [SerializeField] private float defaultSize;
    [Tooltip("摄像机最大尺寸")]
    [SerializeField] private float maxSize;
    
    void Awake()
    {
        Players1 = GameObject.FindGameObjectWithTag("Pencil").transform;
        Players2 = GameObject.FindGameObjectWithTag("Eraser").transform;
        mainCamera = GetComponent<CinemachineVirtualCamera>();
        defaultSize = mainCamera.m_Lens.OrthographicSize;
    }

    void Update()
    {
        transform.position = (Players1.position + Players2.position) / 2 + new Vector3(0, 0, -10);
        View();
    }
    void View()
    {
        //放大视野
        float distanceBetweenPlayers = (Players1.position - Players2.position).sqrMagnitude;
        //玩家距离过大
        if(distanceBetweenPlayers > maxDistance)
        {
            mainCamera.m_Lens.OrthographicSize = defaultSize / maxDistance * distanceBetweenPlayers;
            if (mainCamera.m_Lens.OrthographicSize > maxSize)
                mainCamera.m_Lens.OrthographicSize = maxSize;
        }
        else
        {
            mainCamera.m_Lens.OrthographicSize = defaultSize;
        }



        //缩小视野
    }
}