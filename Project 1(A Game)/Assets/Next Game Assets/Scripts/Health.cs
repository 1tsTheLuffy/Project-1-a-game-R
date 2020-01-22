using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject destroyParticle;

    private void OnDestroy()
    {
        destroyParticle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(destroyParticle, 2f);
    }
}
