using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_Fly : MonoBehaviour
{
    
    [SerializeField] private GameObject flower_CanFly;
    [SerializeField] private GameObject flower_CannotFly;
    [SerializeField] private Transform[] move_Flower;
    [SerializeField] private Transform[] static_Flower;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] public float time;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originColor;
    [SerializeField] private Transform finalPosition;
    [SerializeField] private bool createFlower;

    [Header("变脸组件")]
    [SerializeField] private FaceController faceController;
    [SerializeField] private SpriteRenderer cryColor;
    bool colorChanged = false;

    void Start()
    {
        originColor = spriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
    }

    #region 变色
    private void Update()
    {
        if (!colorChanged && faceController.cry)
        {
            StartCoroutine(ChangeColor());
            colorChanged = true;
        }
    }

    IEnumerator ChangeColor()
    {
        float change_Color = Time.deltaTime;

        while (spriteRenderer.color.a > 0)
        {
            if (spriteRenderer.color.a > change_Color)
            {
                spriteRenderer.color -= new Color(0, 0, 0, change_Color);
                cryColor.color += new Color(0, 0, 0, change_Color);
            }

            else
            {
                spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0);
                cryColor.color = new Color(cryColor.color.r, cryColor.color.g, cryColor.color.b, 1);
            }
            yield return new WaitForFixedUpdate();
        }

    }
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Pencil") || collision.CompareTag("Eraser")) && !createFlower)
        {
            if (faceController && !faceController.isUsed)
            {
                faceController.smile = true;
            }


            audioSource.Play();
            CreateFlower();
            StartCoroutine(Destroy());
            //StartCoroutine(ChangePosition(finalPosition.position));
            //StartCoroutine(ChangeTransparency(spriteRenderer.color, 0f));
            //StartCoroutine(ChangeTransparency(cryColor.color, 0f));
        }
        if (!createFlower)
        {
            createFlower = true;
        }
        
    }

    IEnumerator Destroy()
    {
        float change_Color = Time.deltaTime / time;

        Vector3 _finalPosition = finalPosition.position;
        Vector3 dir = (_finalPosition - transform.position).normalized;

        while (spriteRenderer.color.a > 0 || transform.position != _finalPosition || cryColor.color.a > 0)
        {
            {//改变透明度
                if (spriteRenderer.color.a > change_Color)
                    spriteRenderer.color -= new Color(0, 0, 0, change_Color);
                else
                    spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 0);

                if(cryColor.color.a > change_Color)
                    cryColor.color -= new Color(0, 0, 0, change_Color);
                else
                    cryColor.color = new Color(cryColor.color.r, cryColor.color.g, cryColor.color.b, 0);
            }

            {//改变位置
                float distance = Vector3.Distance(transform.position, _finalPosition);
                Vector3 change_Position = Time.deltaTime / time * Mathf.Pow(distance, 1.5f) * dir;
                transform.position += change_Position;
            }

            yield return new WaitForFixedUpdate();
        }
        //yield return new WaitForSeconds(2);
    }

    private void CreateFlower()
    {
        foreach(Transform  pos in move_Flower)
        {
            GameObject flower = Instantiate(flower_CanFly, pos.position, transform.rotation);
            flower.GetComponent<FlyFlower>()._highPosition = pos.GetChild(0).position;
            flower.GetComponent<FlyFlower>()._lowPosition = pos.GetChild(1).position;
            
        }
        foreach (Transform pos in static_Flower)
        {
            Instantiate(flower_CannotFly, pos.position, transform.rotation);
        }
    }

    //IEnumerator ChangePosition(Vector3 distination)
    //{
    //    Vector3 dir = (distination - transform.position).normalized;
    //    while (transform.position != distination)
    //    {
    //        float distance = Vector3.Distance(transform.position, distination);
    //        Vector3 change_Position = Time.deltaTime / time * Mathf.Pow(distance, 1.5f) * dir;
    //        transform.position += change_Position;

    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    //IEnumerator ChangeTransparency(Color color, float transparency)
    //{
    //    float changeTransparency = Time.deltaTime;

    //    if (color.a > transparency)
    //        while (color.a > transparency)
    //        {
    //            if (color.a - changeTransparency < transparency)
    //                color.a = transparency;
    //            else
    //                color.a -= changeTransparency;

    //            yield return new WaitForFixedUpdate();
    //        }
    //    else
    //        while (color.a < transparency)
    //        {
    //            if (color.a + changeTransparency > transparency)
    //                color.a = transparency;
    //            else
    //                color.a += changeTransparency;

    //            yield return new WaitForFixedUpdate();
    //        }
    //}
}
