using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<AudioSource>();
    }
    public void Play()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync("选择关卡");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
