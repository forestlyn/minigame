using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> defaults;
    [SerializeField] private List<GameObject> smiles;
    [SerializeField] private List<GameObject> crys;

    public bool _smile = false;
    public bool _cry = false;
    [SerializeField] private bool isUsed = false;

    private void Start()
    {
        foreach(GameObject item in defaults)
        {
            item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        foreach(GameObject item in smiles)
        {
            item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        foreach (GameObject item in crys)
        {
            item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }

    private void Update()
    {
        if (isUsed)
            return;

        

        if (_smile)
        {
            foreach (GameObject item in defaults)
            {
                Disappear(item.GetComponent<SpriteRenderer>());
            }
            foreach(GameObject item in smiles)
            {
                Appear(item.GetComponent<SpriteRenderer>());
            }
            isUsed = true;
        }
        if (_cry)
        {
            foreach (GameObject item in defaults)
            {
                Disappear(item.GetComponent<SpriteRenderer>());
            }
            foreach (GameObject item in crys)
            {
                Appear(item.GetComponent<SpriteRenderer>());
            }
            isUsed = true;
        }
    }

    private void Appear(SpriteRenderer spriteRenderer)
    {
        StartCoroutine(IAppear(spriteRenderer, 1f));
    }

    private void Disappear(SpriteRenderer spriteRenderer)
    {
        StartCoroutine(IDisappear(spriteRenderer, 0.5f));
    }

    IEnumerator IDisappear(SpriteRenderer spriteRenderer, float time)
    {
        float change_Color = Time.deltaTime / time; 
        
        while (spriteRenderer.color.a > 0)
        {
            {//改变透明度
                if (spriteRenderer.color.a > change_Color)
                    spriteRenderer.color -= new Color(0, 0, 0, change_Color);
                else
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator IAppear(SpriteRenderer spriteRenderer, float time)
    {
        float change_Color = Time.deltaTime / time;

        while (spriteRenderer.color.a < 1)
        {
            {//改变透明度
                if (spriteRenderer.color.a + change_Color < 1)
                    spriteRenderer.color += new Color(0, 0, 0, change_Color);
                else
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
