using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float flyingSpeed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * flyingSpeed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Debug.Log("It is out of screen..");
    }
}
