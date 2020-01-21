using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : MonoBehaviour
{
    public int health;
    [SerializeField] float minDistance;
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float timeBetweenShoot;

    [SerializeField] GameObject bullet;

    private Transform Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("SpaceShip").transform;

        timer = timeBetweenShoot;

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

        transform.RotateAround(Player.position, Vector3.forward, -50 * Time.deltaTime);

        if (timer <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timer = timeBetweenShoot;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (health <= 0)
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
