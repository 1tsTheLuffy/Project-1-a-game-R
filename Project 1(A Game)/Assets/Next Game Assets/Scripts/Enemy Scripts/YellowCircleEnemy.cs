using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCircleEnemy : MonoBehaviour
{
    private int i;
    [SerializeField] float speed;

    [SerializeField] GameObject Health;

    MainPlayer mp;

    [SerializeField] Transform Player;

    private void Start()
    {
        i = Random.Range(1, 4);
        Player = GameObject.FindGameObjectWithTag("SpaceShip").transform;
        mp = GameObject.FindGameObjectWithTag("SpaceShip").GetComponent<MainPlayer>();

        if (Player == null)
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

            if (i == 2)
            {
                Instantiate(Health, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDestroy()
    {
        mp.score++;
    }
}
