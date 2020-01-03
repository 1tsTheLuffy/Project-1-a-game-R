using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] float waitTime;

    [SerializeField] GameObject particle;

    [SerializeField] Transform point;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isTouched", true);
            Instantiate(particle, point.position, Quaternion.identity);
            StartCoroutine(setIdle());
        }
    }


    IEnumerator setIdle()
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isTouched", false);
    }
}
