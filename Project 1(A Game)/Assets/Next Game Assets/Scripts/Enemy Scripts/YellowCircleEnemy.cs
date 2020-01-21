using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCircleEnemy : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Transform Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("SpaceShip").transform;

        if(Player == null)
        {
            return;
        }
    }

    private void Update()
    {
        if (Player == null)
        {
            return;
        }
        float distance = Vector2.Distance(Player.position, transform.position);

        transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NewBulletTag"))
        {
            Destroy(gameObject);
        }
    }
}
