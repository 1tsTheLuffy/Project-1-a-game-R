using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Range(0f,100f)]
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDestroyTime;

    [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);

        Destroy(gameObject, bulletDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WallTag"))
        {
            Destroy(gameObject);
            Debug.Log("Wall..");
        }
    }

    private void OnDestroy()
    {
        GameObject destroyBulletParticle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(destroyBulletParticle, 1f);
    }
}
