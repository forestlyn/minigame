using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [Header("铅笔相关")]
    [SerializeField] private Transform pencil;
    [SerializeField] private Database database;
    [Header("画框")]
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    private float size;
    [Header("遮挡物")]
    [SerializeField] private Transform leftShelter;
    [SerializeField] private Transform rightShelter;
    [SerializeField] private float leftPosition;
    [SerializeField] private float rightPosition;
    [Header("线条贴图")]
    [SerializeField] private Transform lineSprite;
    [Header("单向平台")]
    [SerializeField] private GameObject oneWayPlatform_DrawArea;
    private GameObject oneWayPlatform;
    private int lineNum = 0;


    private float originScale;
    

    private void Start()
    {
        pencil = GameObject.FindGameObjectWithTag("Pencil").transform;
        originScale = leftShelter.localScale.x;
        size = rightBoundary.position.x - leftBoundary.position.x;
        leftPosition = rightBoundary.position.x;
        rightPosition = leftBoundary.position.x;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pencil"))
        {
            if (database.isDrawing)
            {
                leftPosition = Mathf.Min(leftPosition, pencil.position.x);
                rightPosition = Mathf.Max(rightPosition, pencil.position.x);
            }
        }
    }

    private void Update()
    {
        //画线
        if (database.canDrawOrNot && Input.GetKeyDown(KeyCode.Q))
        {
            database.isDrawing = !database.isDrawing;
            if (database.isDrawing && lineNum == 0)
            {
                oneWayPlatform = Instantiate(oneWayPlatform_DrawArea, new Vector3(pencil.position.x, lineSprite.position.y-transform.position.y, 0f), Quaternion.identity, transform);
                lineNum += 1;
            }
        }

        if (database.isDrawing)
        {
            leftShelter.localScale = new Vector3(Mathf.Max(0, leftPosition - leftBoundary.position.x) / size * originScale, leftBoundary.localScale.y, leftBoundary.localScale.z);
            rightShelter.localScale = new Vector3(Mathf.Max(0, rightBoundary.position.x - rightPosition) / size * originScale, rightBoundary.localScale.y, rightBoundary.localScale.z);
            if (oneWayPlatform != null)
            {
                oneWayPlatform.GetComponent<Transform>().localPosition = new Vector3(leftPosition, lineSprite.position.y, 0f);
            }
        }
    }

}
