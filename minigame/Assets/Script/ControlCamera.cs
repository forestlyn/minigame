using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    //[Header("玩家")]
    //[SerializeField] private Transform Players1;
    //[SerializeField] private Transform Players2;
    //[Tooltip("摄像机为初始尺寸时玩家间最大的距离")]
    //[SerializeField] private float maxDistance;

    //[Header("摄像机")]
    //[Tooltip("摄像机")]
    //[SerializeField] private CinemachineVirtualCamera mainCamera;
    //[Tooltip("摄像机位置")]
    //[SerializeField] private Transform cameraPosition;
    //[Tooltip("摄像机初始尺寸")]
    //[SerializeField] private float defaultSize;
    //[Tooltip("摄像机最大尺寸")]
    //[SerializeField] private float maxSize;
    //[Tooltip("摄像机当前大小")]
    //[SerializeField] private float nowDistance;
    //[Tooltip("每次摄像机增大或减小的大小")]
    //[SerializeField] private float changeDistance;



    [SerializeField] private Transform Target;
    void Awake()
    {
        //Players1 = GameObject.FindGameObjectWithTag("Pencil").transform;
        //Players2 = GameObject.FindGameObjectWithTag("Eraser").transform;
        ////mainCamera = GetComponent<CinemachineVirtualCamera>();
        //defaultSize = mainCamera.m_Lens.OrthographicSize;
    }

    void Update()
    {
        transform.position = Target.position;
        //Follow();
        //View();
    }

    //视野缩放
    //void View()
    //{
    //    //玩家间距离
    //    float distanceBetweenPlayers = (Players1.position - Players2.position).sqrMagnitude;
    //    //玩家距离过大
    //    if(distanceBetweenPlayers > maxDistance)
    //    {
    //        mainCamera.m_Lens.OrthographicSize = Mathf.Min(defaultSize / maxDistance * distanceBetweenPlayers,maxSize);
    //    }
    //    else
    //    {
    //        mainCamera.m_Lens.OrthographicSize = defaultSize;
    //    }

    //    /*if (distanceBetweenPlayers > maxDistance && distanceBetweenPlayers > nowDistance - changeDistance)
    //    {
    //        nowDistance = maxDistance + changeDistance;
    //        mainCamera.m_Lens.OrthographicSize = defaultSize / maxDistance * nowDistance;
    //        if (mainCamera.m_Lens.OrthographicSize > maxSize)
    //            mainCamera.m_Lens.OrthographicSize = maxSize;
    //    }
    //    else
    //    {
    //        mainCamera.m_Lens.OrthographicSize = defaultSize;
    //    }*/
    //}

    //void Follow()
    //{
    //    cameraPosition.position = (Players1.position + Players2.position) / 2;
    //}
}