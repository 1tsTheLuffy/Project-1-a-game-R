using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    //[SerializeField] float timer;
    //[SerializeField] float timeBtwUp;

    Animator animator;
    BoxCollider2D bx;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bx = GetComponent<BoxCollider2D>();
        //timer = timeBtwUp;

        StartCoroutine(anim());
    }

    private void Update()
    {
        if(animator.GetBool("isUp") == true)
        {
            bx.enabled = true;
        }else if(animator.GetBool("isUp") == false)
        {
            bx.enabled = false;
        } 
    }

    IEnumerator anim()
    {
        while(true)
        {
            animator.SetBool("isUp", true);
            yield return new WaitForSeconds(5f);
            animator.SetBool("isUp", false);
            yield return new WaitForSeconds(2f);
            animator.SetTrigger("Staydown");
        }
    }
}
