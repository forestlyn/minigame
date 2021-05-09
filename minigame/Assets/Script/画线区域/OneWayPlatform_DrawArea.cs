using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform_DrawArea : MonoBehaviour
{
    [SerializeField] public float size;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    
    private void Awake()
    {
        size = right.position.x - left.position.x;
    }
}
