using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject pauseButton;
    [SerializeField] AudioSource audioSource;
    private void Start()
    {
        pause.SetActive(false);
        audioSource = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<AudioSource>();
    }
    public void Pause()
    {
        audioSource.Play();
        Time.timeScale = 0;
        pause.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ToMainMenu()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync("主菜单");
        Time.timeScale = 1;
    }

    public void RePlay()
    {
        audioSource.Play();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Continue()
    {
        audioSource.Play();
        Time.timeScale = 1;
        pause.SetActive(false);
        pauseButton.SetActive(true);
    }
}
