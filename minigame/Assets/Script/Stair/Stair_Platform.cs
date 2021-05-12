using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair_Platform : MonoBehaviour
{
    [SerializeField] private List<Transform> rayPoints;
    [SerializeField] private float rayLength;
    [SerializeField] private bool isPencilOn;
    private void Update()
    {
        isPencilOn = false;
        Vector2 up = new Vector2(0, 1);
        foreach(Transform item in rayPoints)
        {
            bool _isPencilOn = Physics2D.Raycast(item.position, up, rayLength, 1 << LayerMask.NameToLayer("Pencil"));
            Debug.DrawRay(item.position, up * rayLength, Color.red);
            if(_isPencilOn)
            {
                isPencilOn = true;
                break;
            }
        }
        if (isPencilOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Pencil"))
        {
            transform.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
