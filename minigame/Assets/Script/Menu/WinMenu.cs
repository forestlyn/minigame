using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<AudioSource>();
    }
    public void RePlay()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync("第一关");
    }

    public void ToMainMenu()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync("主菜单");
    }
}
