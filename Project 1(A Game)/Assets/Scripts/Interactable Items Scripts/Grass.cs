using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] float waitTime;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

       // Physics2D.queriesHitTriggers = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Hog") || collision.CompareTag("Duck"))
        {
            animator.SetBool("isTouched", true);
            StartCoroutine(setIdle());
        }
    }

    IEnumerator setIdle()
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isTouched", false);
    }
}
