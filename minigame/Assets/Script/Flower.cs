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

    private Color originColor;
    void Start()
    {
        originColor = spriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pencil") || collision.CompareTag("Eraser"))
        {
            audioSource.Play();
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
}
