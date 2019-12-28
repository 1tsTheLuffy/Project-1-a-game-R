using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [SerializeField] int i;
    [SerializeField] float walkingSpeeed;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        i = Random.Range(1, 17);

        StartCoroutine(animationTransition());
    }

    private void FixedUpdate()
    {
        if(animator.GetBool("isWalking") == true)
        {
            transform.Translate(Vector3.left * walkingSpeeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator animationTransition()
    {
        while(true)
        {
            if (i >= 1 && i <= 3)
            {
                animator.SetBool("isWalking", false);
                yield return new WaitForSeconds(5f);
                i = Random.Range(1, 11);
            }
            else if (i >= 4 && i <= 8)
            {
                Flip();
                animator.SetBool("isWalking", true);
                yield return new WaitForSeconds(5f);
                i = Random.Range(1, 11);
            }else if(i >= 9 && i <= 13)
            {
                Flip();
                animator.SetBool("isWalking", true);
                yield return new WaitForSeconds(5f);
                i = Random.Range(1, 11);
            }else if(i >= 14 && i<= 17)
            {
                animator.SetBool("isEating", true);
                animator.SetBool("isWalking", false);
                yield return new WaitForSeconds(5f);
                i = Random.Range(1, 17);
            }
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
