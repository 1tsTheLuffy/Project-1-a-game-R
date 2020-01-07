using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthworm : MonoBehaviour
{
    [SerializeField] int i;

    [SerializeField] float speed;

    private void Start()
    {
        i = Random.Range(0, 2);
    }

    private void Update()
    {
        if(i == 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }else if(i == 1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
