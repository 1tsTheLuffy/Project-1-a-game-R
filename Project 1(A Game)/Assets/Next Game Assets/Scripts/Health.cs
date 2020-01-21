using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float waitTime;

    [SerializeField] GameObject particle;

    private void Update()
    {
        GameObject d = Instantiate(particle, transform.position, transform.rotation);
        Destroy(d, waitTime);
    }
}
