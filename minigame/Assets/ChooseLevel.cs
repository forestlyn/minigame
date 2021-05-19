using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public void Play1()
    {
        SceneManager.LoadSceneAsync("第一关");
    }

    public void Play2()
    {
        SceneManager.LoadSceneAsync("第二关");
    }

    public void Play3()
    {
        SceneManager.LoadSceneAsync("第三关");
    }
}
