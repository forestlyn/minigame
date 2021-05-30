using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Flower : MonoBehaviour
{
    [SerializeField] private Transform finalPosition;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float time = 2f;
    [SerializeField] private AudioSource audioSource;

    [Header("变脸组件")]
    [SerializeField] private FaceController faceController;
    bool isUsed = false;
    bool colorChanged = false;

    private Color originColor;
    void Start()
    {
        originColor = spriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!faceController)
            return;

        if (!colorChanged && faceController.cry)//(0,204/255,1,1)
        {
            StartCoroutine(ChangeColor());
            colorChanged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pencil") || collision.CompareTag("Eraser"))
        {
            if (!isUsed)
            {
                audioSource.Play();
                isUsed = true;
            }
            
            StartCoroutine(GetFlower());
        }
    }

    IEnumerator GetFlower()
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

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ChangeColor()
    {
        float change_r = Time.deltaTime * (0f - spriteRenderer.color.r);
        float change_g = Time.deltaTime * (0.7981f - spriteRenderer.color.g);
        float change_b = Time.deltaTime * (1f - spriteRenderer.color.b);
        float change_a = Time.deltaTime * (1f - spriteRenderer.color.a);

        while (spriteRenderer.color.r > 0)
        {
            if (spriteRenderer.color.r + change_r < 0)
            {
                Debug.Log("1");
                spriteRenderer.color = new Color(0, 0.7981f, 1, 1);
            }  
            else
            {
                Debug.Log("2");
                Debug.Log("flowerColor.r  "+ spriteRenderer.color.r);
                spriteRenderer.color += new Color(change_r, change_g, change_b, change_a);
                
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
