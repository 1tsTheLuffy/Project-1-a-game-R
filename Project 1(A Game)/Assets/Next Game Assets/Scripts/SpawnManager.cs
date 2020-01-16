using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int index;
    
    [SerializeField] GameObject Enemy;

    [SerializeField] Transform[] spawnPoints;

    private void Start()
    {
        index = Random.Range(0, spawnPoints.Length);

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            Instantiate(Enemy, spawnPoints[index].position, Quaternion.identity);
            index = Random.Range(0, spawnPoints.Length);

            yield return new WaitForSeconds(5f);
        }
    }
}
