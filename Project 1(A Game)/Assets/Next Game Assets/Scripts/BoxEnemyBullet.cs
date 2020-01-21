using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 playerPos;
    Transform Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("SpaceShip").transform;
        playerPos = new Vector2(Player.position.x, Player.position.y);
    }

    private void Update()
    {
        if(transform.position.x == playerPos.x && transform.position.y == playerPos.y)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.fixedDeltaTime);
    }
}
