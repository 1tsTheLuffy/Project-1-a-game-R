using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hog : MonoBehaviour
{
    private int randomPoint;
    [SerializeField] bool isRight;
    [SerializeField] float movementSpeed;
    [SerializeField] float runSpeed;

    private Transform Player;
    [SerializeField] Transform[] movePoints;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        // randomPoint = Random.Range(0, movePoints.Length);
        randomPoint = 1;
    }

    private void Update()
    {

        float distance = Vector2.Distance(transform.position, Player.position);
        Debug.Log(distance);
        if(distance <= 6f)
        {
            if(Player.position.x > transform.position.x && Player.position.y > transform.position.y)
            {
              //  transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
                Debug.Log("Right!!");
            }
            else if(Player.position.x < transform.position.x && Player.position.y < transform.position.y)
            {
              //  transform.Translate(-Vector3.right * runSpeed * Time.deltaTime);
                Debug.Log("Left!!");
            }
        }
        else
        {
            if (!isRight)
            {
                transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
                //Flip();
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
                //Flip();
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
    }

    private void Flip()
    {
        Vector2 faceDirection = transform.localScale;
        faceDirection.x *= -1;
        transform.localScale = faceDirection;
    }
}
