using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    private Transform Players1;
    private Transform Players2;

    private Camera MainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        Players1 = GameObject.FindGameObjectWithTag("Pencil").transform;
        Players2 = GameObject.FindGameObjectWithTag("Eraser").transform;
        MainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Players1.position + Players2.position) / 2 + new Vector3(0, 0, -10);
        View();
    }
    void View()
    {
        //放大视野


       



        //缩小视野
    }
}