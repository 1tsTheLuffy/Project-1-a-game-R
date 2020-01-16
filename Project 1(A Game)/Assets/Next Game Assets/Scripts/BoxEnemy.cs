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
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, Player.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
