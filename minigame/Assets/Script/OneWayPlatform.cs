using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private Transform layPoint1;
    [SerializeField] private Transform layPoint2;
    [SerializeField] private Transform layPoint3;

    [SerializeField] private Collider2D collPencil;
    [SerializeField] private Collider2D[] collErasers;

    [SerializeField] private bool pencil1;
    [SerializeField] private bool eraser1;
    [SerializeField] private bool pencil2 = true;
    [SerializeField] private bool eraser2 = true;

    private Vector3 dir;
    private float distance;

    private void Start()
    {
        
        distance = Vector3.Distance(layPoint1.position, layPoint2.position);

        collPencil = GameObject.FindGameObjectWithTag("Pencil").GetComponent<Collider2D>();
        collErasers = GameObject.FindGameObjectWithTag("Eraser").GetComponents<Collider2D>();

    }

    private void Update()
    {dir = (layPoint2.position - layPoint1.position).normalized;
        pencil1 = Physics2D.Raycast(layPoint1.position, dir, distance, 1 << LayerMask.NameToLayer("Pencil"));
        eraser1 = Physics2D.Raycast(layPoint1.position, dir, distance, 1 << LayerMask.NameToLayer("Eraser"));

        pencil2 = !Physics2D.Raycast(layPoint3.position, dir, distance, 1 << LayerMask.NameToLayer("Pencil"));
        eraser2 = !Physics2D.Raycast(layPoint3.position, dir, distance, 1 << LayerMask.NameToLayer("Eraser"));

        if (pencil1 && pencil2)
            Physics2D.IgnoreCollision(collPencil, transform.GetComponent<Collider2D>(), false);
        else
            Physics2D.IgnoreCollision(collPencil, transform.GetComponent<Collider2D>(), true);

        if (eraser1 && eraser2)
            for (int i = 0; i < collErasers.Length; i++)
                Physics2D.IgnoreCollision(collErasers[i], transform.GetComponent<Collider2D>(), false);
        else
            for (int i = 0; i < collErasers.Length; i++)
                Physics2D.IgnoreCollision(collErasers[i], transform.GetComponent<Collider2D>(), true);

        Debug.DrawRay(layPoint1.position, dir * distance, Color.green);
        Debug.DrawRay(layPoint3.position, dir * distance, Color.green);

    }
}
