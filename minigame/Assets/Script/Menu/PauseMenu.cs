using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject pauseButton;
    // Start is called before the first frame update
    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("主菜单");
        Time.timeScale = 1;
    }

    public void RePlay()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        pauseButton.SetActive(true);
    }
}
