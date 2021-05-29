using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private FaceController faceController;
    [SerializeField] private float rayLength;
    [Tooltip("判断是否发出声音的临界速度（+）")]
    [SerializeField] private float speed_Audio;
    [Tooltip("变脸的临界速度")]
    [SerializeField] private float speed_Transform;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("BallPosition").GetComponent<Collider2D>(), transform.GetComponent<Collider2D>(),true);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        bool isOnGround = false;
        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayLength, 1 << LayerMask.NameToLayer("Map"));
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.blue);

        if (isOnGround)
        {
            if (rb.velocity.y < -speed_Transform)
            {
                if (!faceController.isUsed)
                    faceController.cry = true;
            }
            
            if (rb.velocity.y < -speed_Audio)
            {
                if (audioSource)
                {
                    audioSource.volume = Mathf.Abs(rb.velocity.y) / (speed_Audio * 30);
                    audioSource.Play();
                }
                    

                Rebound();
            }
        }
    }

    void Rebound()
    {
        rb.velocity = new Vector2(rb.velocity.x, -0.5f * rb.velocity.y);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("BallPosition"))
        {
            Physics2D.IgnoreCollision(collision.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>(), false);
        }
    }

}
