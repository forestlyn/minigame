using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform_DrawArea : MonoBehaviour
{
    [SerializeField] public float size;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private GameObject pencil;
    [SerializeField] private Collider2D _collider;

    private void Awake()
    {
        size = right.position.x - left.position.x;
    }

    private void Start()
    {
        pencil = GameObject.FindGameObjectWithTag("Pencil");
        Physics2D.IgnoreCollision(_collider, pencil.GetComponent<Collider2D>());
    }
}
