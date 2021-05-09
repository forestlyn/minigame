using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErasableLine : MonoBehaviour
{
    [Header("射线相关")]
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayLength;
    [SerializeField] private bool isWiping;

    [Header("线条相关")]
    [SerializeField] private Database database;
    [SerializeField] private Transform upBoundary;
    [SerializeField] private Transform downBoundary;
    [SerializeField] private float length;
    [SerializeField] private float speed;

    private void Start()
    {
        length = upBoundary.position.y - downBoundary.position.y;
        speed = 4 / length * transform.localScale.y;
    }

    void Update()
    {
        IsWiping();
        Wipe();
    }

    private void IsWiping()
    {
        Vector2 down = new Vector2(0, -1);
        if (Physics2D.Raycast(rayPoint.position, down, rayLength, 1 << LayerMask.NameToLayer("Eraser")))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                database.isWiping = !database.isWiping;
            }
        }
        else database.isWiping = false;
        Debug.DrawRay(rayPoint.position, down * rayLength, Color.black);
    }

    private void Wipe()
    {
        if (database.isWiping)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - speed * Time.deltaTime >= 0 ? transform.localScale.y - speed * Time.deltaTime : 0, transform.localScale.z);
        }

    }
}
