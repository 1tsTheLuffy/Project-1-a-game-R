using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialougeTrigger : MonoBehaviour
{
    [SerializeField] float minDistance;

    [SerializeField] Transform GrandpaPos;

    [SerializeField] GameObject pressImage;

    [SerializeField] DialougeTrigger[] dialougeTrigger;

    [SerializeField] DialougeManager dm;
    
    private void Start()
    {
        GrandpaPos = GameObject.FindGameObjectWithTag("Grandpa").transform;
        pressImage.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, GrandpaPos.position);

        if(distance <= minDistance && dm.animator.GetBool("IsOpen") == false)
        {
            pressImage.SetActive(true);
        }else if(distance > minDistance)
        {
            pressImage.SetActive(false);
        }

        if(distance <= minDistance && Input.GetKeyDown(KeyCode.E))
        {
            dialougeTrigger[0].Trigger();
        }

        if(dm.animator.GetBool("IsOpen") == true)
        {
            pressImage.SetActive(false);
        }
    }
}
