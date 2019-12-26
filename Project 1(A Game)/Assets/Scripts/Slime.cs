using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private bool isGrounded;
    [SerializeField] float jumpForce;
    [SerializeField] float radius;
    [SerializeField] float destroyTime;

    public GameObject instance;
    [SerializeField] GameObject bounceParticle;

    [SerializeField] Transform bouncePoint;

    [SerializeField] LayerMask Ground;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(bouncePoint.position, radius, Ground);

        Destroy(instance, destroyTime);
    }

    private void FixedUpdate()
    {
        if(isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;

            instance = Instantiate(bounceParticle, bouncePoint.position, Quaternion.identity);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Base"))
    //    {
    //        instance = Instantiate(bounceParticle, bouncePoint.position, Quaternion.identity);
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(bouncePoint.position, radius);
    }
}
