using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFlower : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private float waitingTime;
    [SerializeField] private float upDown;
    [SerializeField] private float speed;
    [SerializeField] private float time;


    void Update()
    {
        UpAndDown();
    }

    private void UpAndDown()
    {
        transform.position += new Vector3(0, 1 * speed * upDown * Time.deltaTime, 0);

        if (transform.position.y >= pos1.position.y || transform.position.y <= pos2.position.y)
        {
            upDown = 0;
            time += Time.deltaTime;
            if (time > waitingTime)
            {
                upDown = transform.position.y >= pos1.position.y ? -1 : 1;
                time = 0;
            }
        }
    }
}
