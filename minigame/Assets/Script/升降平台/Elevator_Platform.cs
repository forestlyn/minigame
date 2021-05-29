using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Platform : MonoBehaviour
{
    [Header("平台")]
    [SerializeField] private Transform highPosition;
    [SerializeField] private Transform lowPosition;
    [Space]
    [SerializeField] private float moveSpeed;
    [SerializeField] public bool isTouched;
    [SerializeField] public bool isTouched1 = false;
    [SerializeField] public bool isTouched2 = false;
    [SerializeField] private AudioSource audioSource;

    float highPosition_Y;
    float lowPosition_Y;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        highPosition_Y = highPosition.position.y;
        lowPosition_Y = lowPosition.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouched1 || isTouched2)
            isTouched = true;
        else
            isTouched = false;


        if (isTouched)
            PlatformUp();
        else
            PlatformDown();
    }

    private void PlatformUp()
    {
        if (transform.position.y <= highPosition_Y)
        {
            transform.position += new Vector3(0, 5 * moveSpeed * Time.deltaTime, 0);
            if (isMoving == false)
                audioSource.Play();
            isMoving = true;
        }
        else
        {
            if (isMoving == true)
                audioSource.Stop();
            isMoving = false;
        }
            
    }

    private void PlatformDown()
    {
        if (transform.position.y >= lowPosition_Y)
        {
            transform.position += new Vector3(0, -5 * moveSpeed * Time.deltaTime, 0);
            if (isMoving == false)
                audioSource.Play();
            isMoving = true;
        }
        else
        {
            if (isMoving == true)
                audioSource.Stop();
            isMoving = false;
        }
    }
}
