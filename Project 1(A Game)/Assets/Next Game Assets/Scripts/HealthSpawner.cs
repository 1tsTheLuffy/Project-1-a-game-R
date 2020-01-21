using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    int i;
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] GameObject Health;

    [SerializeField] Transform[] spawnPoints;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        i = Random.Range(0, spawnPoints.Length);

        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while(true)
        {
            Instantiate(Health, spawnPoints[i].position, Quaternion.identity);
            float delay = Random.Range(x, y);
            yield return new WaitForSeconds(delay);
        }
    }
}
