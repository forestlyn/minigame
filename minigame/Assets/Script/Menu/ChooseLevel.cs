using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<AudioSource>();
    }
    public void Play1()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync("第一关");
    }

    public void Play2()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync("第二关");
    }

}
