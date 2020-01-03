using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hog : MonoBehaviour
{
    private int randomPoint;
    [SerializeField] bool isRight;
    [SerializeField] float movementSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float rayDistance;

    private Transform Player;
    [SerializeField] Transform[] movePoints;

    [SerializeField] LayerMask PlayerMask;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        Physics2D.queriesStartInColliders = false;
        // randomPoint = Random.Range(0, movePoints.Length);
        randomPoint = 1;
    }

    private void Update()
    {

        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, PlayerMask);
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, -Vector2.right, rayDistance, PlayerMask);
        if (raycastRight.collider != null)
        {
            if(raycastRight.collider.CompareTag("Player"))
            {
                transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
                transform.localScale = new Vector2(-1, 1);
                animator.SetBool("isRunning", true);
                Debug.Log(raycastRight.transform.name + "Right Player..");
            }
         //   else if(raycastRight.collider.CompareTag("Player"))
          //  {
           //     Physics2D.IgnoreCollision(collider1 : raycastRight.collider, collider2 : raycastRight.collider);
            //}
        }
        else if (raycastLeft.collider != null)
        {
            if (raycastLeft.collider.CompareTag("Player"))
            {
                transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
                transform.localScale = new Vector2(1, 1);
                animator.SetBool("isRunning", true);
                // Flip();
                Debug.Log(raycastLeft.transform.name + "Left Player..");
            }
     //       else if (raycastRight.collider.CompareTag("Player"))
      //      {
       //         Physics2D.IgnoreCollision(collider1: raycastRight.collider, collider2: raycastRight.collider);
         //   }
        }
        else
        {
            animator.SetBool("isRunning", false);
            if (!isRight)
            {
                transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector2(1, 1);
            }

            if (Vector2.Distance(transform.position, movePoints[1].position) < 1f)
            {
                isRight = true;
            }
            if (Vector2.Distance(transform.position, movePoints[0].position) < 1f)
            {
                isRight = false;
            }
        }

        //// Right Raycast.(but there is a problem here).
 
    }

    private void Flip()
    {
        Vector2 faceDirection = transform.localScale;
        faceDirection.x *= -1;
        transform.localScale = faceDirection;
    }
}
