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
    [SerializeField] public float leftPosition;
    [SerializeField] public float rightPosition;
    [Header("线条贴图")]
    [SerializeField] private Transform lineSprite;
    [Header("单向平台")]
    [SerializeField] private GameObject oneWayPlatform_DrawArea;
    private GameObject oneWayPlatform;
    private int lineNum = 0;
    private float platformSize;


    private float originScale;

    [Tooltip("记录画框是否使用过")]
    [SerializeField] private bool used;
    

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
            
            if(!used)
                database.isDrawing = !database.isDrawing;
            if (!database.isDrawing)
                used = true;
            if (database.isDrawing && lineNum == 0)
            {
                oneWayPlatform = Instantiate(oneWayPlatform_DrawArea, new Vector3(pencil.position.x, lineSprite.position.y, 0f), Quaternion.identity);
                lineNum += 1;
                platformSize = oneWayPlatform.GetComponent<OneWayPlatform_DrawArea>().size;
            }
        }

        if (database.isDrawing && !used)
        {
            leftShelter.localScale = new Vector3(Mathf.Max(0, leftPosition - leftBoundary.position.x) / size * originScale, leftBoundary.localScale.y, leftBoundary.localScale.z);
            rightShelter.localScale = new Vector3(Mathf.Max(0, rightBoundary.position.x - rightPosition) / size * originScale, rightBoundary.localScale.y, rightBoundary.localScale.z);
            if (oneWayPlatform != null)
            {
                oneWayPlatform.GetComponent<Transform>().localPosition = new Vector3(leftPosition, lineSprite.position.y, 0f);
                oneWayPlatform.GetComponent<Transform>().localScale = new Vector3(Mathf.Max(rightPosition - leftPosition, 0) / platformSize* 1, oneWayPlatform.GetComponent<Transform>().localScale.y, oneWayPlatform.GetComponent<Transform>().localScale.z);
            }
        }
    }

}
