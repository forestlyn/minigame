using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Button : MonoBehaviour
{
    [Header("平台")]
    [SerializeField] private Elevator_Platform platform_script;
    [Header("按钮")]
    [SerializeField] private int Button_ID;
    [SerializeField] private List<Transform> rayPoints; 
    [SerializeField] private Transform upPosition;
    [SerializeField] private Transform downPosition;
    [Space]
    [SerializeField] private float moveSpeed;
    [Space]
    [SerializeField] private float rayLength;

    Vector2 up = new Vector2(0, 1);
    
    float upPosition_Y;
    float downPosition_Y;
    [SerializeField]
    bool isTouched;

    private void Start()
    {
        
        upPosition_Y = upPosition.position.y;
        downPosition_Y = downPosition.position.y;
    }

    private void FixedUpdate()
    {
        isTouched = false;
        foreach (Transform item in rayPoints)
        {
            Debug.DrawRay(item.position, up, Color.blue);
            if (!isTouched)
                isTouched = Physics2D.Raycast(item.position, up, rayLength, 1 << LayerMask.NameToLayer("Pencil"));
            if (!isTouched)
                isTouched = Physics2D.Raycast(item.position, up, rayLength, 1 << LayerMask.NameToLayer("Eraser"));
        }

        if (Button_ID == 1)
            platform_script.isTouched1 = isTouched;
        else if (Button_ID == 2)
            platform_script.isTouched2 = isTouched;
        else
            Debug.Log("error:按钮ID错误");
        
        if (isTouched)
        {
            Buttondown();
        }
        else
        {
            ButtonUp();
        }
    }



    private void ButtonUp()
    {
        if(transform.position.y <= upPosition_Y)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    private void Buttondown()
    {
        if(transform.position.y >= downPosition_Y)
        {
            transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        }
    }

}
