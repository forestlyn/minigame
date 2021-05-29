using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{

    #region 单例
    public static BGMController instance { get; private set; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private BGMList bgmList;
    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        int presentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(presentIndex);
        switch (presentIndex)
        {
            case 0:
            case 1:
            case 4:
                {
                    
                    if (audioSource.clip != bgmList.BGMClips[0])
                    {
                        audioSource.clip = bgmList.BGMClips[0];
                        audioSource.Play();
                    }
                    break;
                }
                
            case 2:
                {
                    if (audioSource.clip != bgmList.BGMClips[1])
                    {
                        audioSource.clip = bgmList.BGMClips[1]; 
                        audioSource.Play();
                    }
                        
                    break;
                }
            case 3:
                {
                    if (audioSource.clip != bgmList.BGMClips[2])
                    {
                        audioSource.clip = bgmList.BGMClips[2];
                        audioSource.Play();
                    }
                        
                    break;
                }
            default:
                Debug.Log("123");
                break;
        }
    }
}
