using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("平台")]
    [SerializeField] private Transform platform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Pencil") || collision.transform.CompareTag("Eraser"))
        {

        }
    }
}
