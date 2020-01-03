using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] private bool isGrounded;
    [SerializeField] int i;
    [SerializeField] float jumpForce;

    [SerializeField] Vector2 jumpDirection;

    [SerializeField] Transform jumpPoint;

    [SerializeField] LayerMask Ground;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(animationTransition());

        i = Random.Range(1, 6);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, 0.1f, Ground);
    }

    IEnumerator animationTransition()
    {
       // while(true)
       // {
            if (i >= 1 && i <= 3)
            {
                if (isGrounded)
                {
                    animator.SetBool("isTakeOff", true);
                animator.SetBool("isTakeOff", true);
                if (animator.GetBool("isTakeOff") == true)
                {
                    Vector2 forceToAdd = new Vector2(jumpDirection.x * jumpForce * Time.fixedDeltaTime,
                        jumpDirection.y * jumpForce * Time.fixedDeltaTime);
                    rb.AddForce(forceToAdd);
                }
                if (!isGrounded)
                {
                    animator.SetBool("isJumping", true);
                    animator.SetBool("isTakeOff", false);
                }
                yield return new WaitForSeconds(5f);
                    i = Random.Range(1, 6);
                }
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isTakeOff", false);
            }
        //}
    }

    //private void FixedUpdate()
    //{
    //   if(Input.GetKeyDown(KeyCode.Space))
    //    {
             
    //    }
    //}
}
