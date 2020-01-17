using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : MonoBehaviour
{
    public int health;
    [SerializeField] float minDistance;
    [SerializeField] float speed;

    private Transform Player;

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
        if(Player == null)
        {
            return;
        }
        if(Vector2.Distance(transform.position, Player.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NewBulletTag"))
        {
            health -= 1;
        }
    }
}
