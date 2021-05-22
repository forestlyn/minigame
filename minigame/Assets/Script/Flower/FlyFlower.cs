using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFlower : MonoBehaviour
{
    [SerializeField] private float waitingTime;
     private float upDown = 1;
    [SerializeField] private float speed;
    private float time;

    public Vector3 _highPosition;
    public Vector3 _lowPosition;


    void Update()
    {
        UpAndDown();
    }

    private void UpAndDown()
    {
        transform.position += new Vector3(0, speed * upDown * Time.deltaTime, 0);

        if (transform.position.y >= _highPosition.y || transform.position.y <= _lowPosition.y)
        {
            upDown = 0;
            time += Time.deltaTime;
            if (time > waitingTime)
            {
                upDown = transform.position.y >= _highPosition.y ? -1 : 1;
                time = 0;
            }
        }
    }
}
