using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    //[SerializeField] float timer;
    //[SerializeField] float timeBtwUp;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //timer = timeBtwUp;

        StartCoroutine(anim());
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
