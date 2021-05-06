using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private Transform upPosition;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Pencil"))
        if (Input.GetKeyDown(KeyCode.E))
        {
            collision.transform.position = upPosition.position;
        }
    }
}
