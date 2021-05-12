using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair_UpTrigger : MonoBehaviour
{
    [SerializeField] private Transform pencil;
    private Rigidbody2D rb;
    [Tooltip("上升所到达的地点")]
    [SerializeField] private Transform upPosition;
    [Tooltip("是否使用纸筒")]
    [SerializeField] private bool isUsing;
    [SerializeField] private Database database;

    private void Start()
    {
        pencil = GameObject.FindGameObjectWithTag("Pencil").transform;
        rb = pencil.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isUsing)
        {
            if (upPosition.position.y - pencil.position.y > 3)
            {
                Vector2 direction = (upPosition.position - pencil.position).normalized;
                rb.velocity = direction * database.defaultJumpSpeed;
            }
        }

        if(pencil.position.y > upPosition.position.y)
        {
            isUsing = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Pencil"))
            if (Input.GetKey(KeyCode.E) && rb.velocity.y > 0)
            {
                //collision.transform.position = upPosition.position;
                isUsing = true;
            }
    }

    
}
