using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouse : MonoBehaviour
{
    [SerializeField] private GameObject crouse;
    [SerializeField] private GameObject theCrouse;
    [SerializeField]private bool show_Pencil;
    [SerializeField]private bool show_Eraser;

    private void Update()
    {
        if((show_Eraser || show_Pencil))
        {
            crouse.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Pencil"))
        {
            show_Pencil = true;
        }
        if (collision.transform.CompareTag("Eraser"))
        {
            show_Eraser = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Pencil"))
        {
            show_Pencil = false;
        }
        if (collision.transform.CompareTag("Eraser"))
        {
            show_Eraser = false;
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        DestoryTheCrouse();
    }
    public void DestoryTheCrouse()
    {
        Destroy(theCrouse);
    }
}
