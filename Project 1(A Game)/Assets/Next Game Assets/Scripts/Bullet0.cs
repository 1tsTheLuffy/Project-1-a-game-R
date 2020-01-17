using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet0 : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RedEnemy") || collision.CompareTag("BlueSquareEnemy"))
        {
            Destroy(gameObject);
        }
    }
}
