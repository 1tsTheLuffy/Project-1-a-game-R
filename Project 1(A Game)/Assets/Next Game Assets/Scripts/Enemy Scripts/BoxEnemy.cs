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

    private GameObject d;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject destroyParticle;

    MainPlayer mp;

    private Transform Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("SpaceShip").transform;
        mp = GameObject.FindGameObjectWithTag("SpaceShip").GetComponent<MainPlayer>();

        timer = timeBetweenShoot;

        if(Player == null)
        {
            return;
        }
    }

    private void Update()
    {

        transform.Rotate(0f, 0f, 2f);

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
            d = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            mp.score++;
        }

        Destroy(d, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NewBulletTag"))
        {
            health -= 1;
        }
    }
}
