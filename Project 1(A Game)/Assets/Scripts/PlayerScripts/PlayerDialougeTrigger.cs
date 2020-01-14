using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialougeTrigger : MonoBehaviour
{
    [SerializeField] float minDistance;

    [SerializeField] Transform GrandpaPos;
    [SerializeField] Transform Doctor;
    [SerializeField] Transform NPC2;
    [SerializeField] Transform NPC3;
    [SerializeField] Transform NPC4;

    [SerializeField] GameObject pressImage;
    [SerializeField] GameObject playGameButton;

    [SerializeField] DialougeTrigger[] dialougeTrigger;

    [SerializeField] DialougeManager dm;

    PlayerController Player;
    
    private void Start()
    {
        GrandpaPos = GameObject.FindGameObjectWithTag("Grandpa").transform;
        Doctor = GameObject.FindGameObjectWithTag("Doctor").transform;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        NPC2 = GameObject.FindGameObjectWithTag("Grandma").transform;
        NPC3 = GameObject.FindGameObjectWithTag("Teacher").transform;
        NPC4 = GameObject.FindGameObjectWithTag("NPC4").transform;

       // dialougeTrigger[0].Trigger();
        pressImage.SetActive(false);
        playGameButton.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, GrandpaPos.position);
        float distanceFromDoctor = Vector2.Distance(transform.position, Doctor.position);
        float distanceFromNPC2 = Vector2.Distance(transform.position, NPC2.position);
        float distanceFromNPC3 = Vector2.Distance(transform.position, NPC3.position);
        float distanceFromNPC4 = Vector2.Distance(transform.position, NPC4.position);

        //* Converstaion With Grandpa./
        if(distance <= 1 && dm.animator.GetBool("IsOpen") == false)
        {
            pressImage.SetActive(true);
        }else if(distance > 1)
        {
            pressImage.SetActive(false);
        }

        if(distance <= 1 && Input.GetKeyDown(KeyCode.E) && dm.animator.GetBool("IsOpen") == false)
        {
            dialougeTrigger[0].Trigger();
        }

        if(dm.animator.GetBool("IsOpen") == true)
        {
            pressImage.SetActive(false);
        }
        //*

        //*Conversation With Doctor..
        if(distanceFromDoctor < 20f)
        {
            if (distanceFromDoctor <= 1f && dm.animator.GetBool("IsOpen") == false)
            {
                pressImage.SetActive(true);
            }
            else if (distanceFromDoctor > 1f)
            {
                pressImage.SetActive(false);
            }
        }

        if (distanceFromDoctor < 1f && Input.GetKeyDown(KeyCode.E) && dm.animator.GetBool("IsOpen") == false && Player.coins != 20)
        {
            dialougeTrigger[1].Trigger();
        }

        if (distanceFromDoctor < 1f && Input.GetKeyDown(KeyCode.E) && dm.animator.GetBool("IsOpen") == false && Player.coins == 20)
        {
            dialougeTrigger[2].Trigger();
        }

        if(distanceFromDoctor < 4f && dm.animator.GetBool("IsOpen") == true && Player.coins == 20)
        {
            playGameButton.SetActive(true);
        }
        //*

        //* Conversation with Grandma.
        if(distanceFromNPC2 < 1f && Input.GetKeyDown(KeyCode.E) && dm.animator.GetBool("IsOpen") == false)
        {
            dialougeTrigger[3].Trigger();
        }

        if (distanceFromNPC2 < 20f)
        {
            if (distanceFromNPC2 <= 1f && dm.animator.GetBool("IsOpen") == false)
            {
                pressImage.SetActive(true);
            }
            else if (distanceFromNPC2 > 1f)
            {
                pressImage.SetActive(false);
            }
        }
        //*

        //* Conversation With teacher.
        if(distanceFromNPC3 < 1f && Input.GetKeyDown(KeyCode.E) && dm.animator.GetBool("IsOpen") == false)
        {
            dialougeTrigger[4].Trigger();
        }

        if (distanceFromNPC3 < 20f)
        {
            if (distanceFromNPC3 <= 1f && dm.animator.GetBool("IsOpen") == false)
            {
                pressImage.SetActive(true);
            }
            else if (distanceFromNPC3 > 1f)
            {
                pressImage.SetActive(false);
            }
        }
        //*

        //* Conversation with NPC4.
        if(distanceFromNPC4 < 1f && Input.GetKeyDown(KeyCode.E) && dm.animator.GetBool("IsOpen") == false)
        {
            dialougeTrigger[5].Trigger();
        }

        if (distanceFromNPC4 < 20f)
        {
            if (distanceFromNPC4 <= 1f && dm.animator.GetBool("IsOpen") == false)
            {
                pressImage.SetActive(true);
            }
            else if (distanceFromNPC4 > 1f)
            {
                pressImage.SetActive(false);
            }
        }
        //*
    }
}
