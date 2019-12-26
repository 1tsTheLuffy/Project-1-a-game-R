using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    private int randomInt;
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwNextMove;

    [SerializeField] Transform[] movepoints;

    private void Start()
    {
        randomInt = Random.Range(0, movepoints.Length);
        timer = timeBtwNextMove;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(movepoints[randomInt].position.x - transform.position.x, 
            movepoints[randomInt].position.y - transform.position.y);
        transform.up = direction;
        transform.position = Vector2.MoveTowards(transform.position, movepoints[randomInt].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movepoints[randomInt].position) < 0.2f)
        {
            if(timer <= 0)
            {
                randomInt = Random.Range(0, movepoints.Length);
                Vector2 directionToNextMovepoint = new Vector2(movepoints[randomInt].position.x - transform.position.x,
            movepoints[randomInt].position.y - transform.position.y);
                transform.up = directionToNextMovepoint;
                timer = timeBtwNextMove;
            }else
            {
                timer -= Time.deltaTime;
            }
        }
        
    }
}
