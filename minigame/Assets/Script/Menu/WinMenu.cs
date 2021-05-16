using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void RePlay()
    {
        SceneManager.LoadSceneAsync("第一关");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("主菜单");
    }
}
