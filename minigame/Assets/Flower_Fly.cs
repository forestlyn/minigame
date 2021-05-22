using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_Fly : MonoBehaviour
{
    
    [SerializeField] private GameObject flower_CanFly;
    [SerializeField] private GameObject flower_CannotFly;
    [SerializeField] private Transform[] poss1;
    [SerializeField] private Transform[] poss2;

    [SerializeField] public float time;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originColor;
    [SerializeField] private Transform finalPosition;
    [SerializeField] private bool createFlower;



    void Start()
    {
        originColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pencil") || collision.CompareTag("Eraser"))
        {
            StartCoroutine(Destroy());
        }

        if (!createFlower)
        {
            CreateFlower();
            createFlower = true;
        }
        
    }


    IEnumerator Destroy()
    {
        float change_Color = Time.deltaTime / time;

        Vector3 _finalPosition = finalPosition.position;
        Vector3 dir = (_finalPosition - transform.position).normalized;

        while (spriteRenderer.color.a > 0)
        {
            {//改变透明度
                if (spriteRenderer.color.a > change_Color)
                    spriteRenderer.color -= new Color(0, 0, 0, change_Color);
                else
                    spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0);
            }

            {//改变位置
                float distance = Vector3.Distance(transform.position, _finalPosition);
                Vector3 change_Position = Time.deltaTime / time * Mathf.Pow(distance, 1.5f) * dir;
                transform.position += change_Position;
            }

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2);
    }

    private void CreateFlower()
    {
        foreach(Transform  pos in poss1)
        {
            Instantiate(flower_CanFly, pos.position, transform.rotation);
        }
        foreach (Transform pos in poss2)
        {
            Instantiate(flower_CannotFly, pos.position, transform.rotation);
        }
    }
}
