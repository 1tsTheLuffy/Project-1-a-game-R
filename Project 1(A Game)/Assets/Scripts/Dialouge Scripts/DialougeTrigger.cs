﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;

    public void Trigger()
    {
        FindObjectOfType<DialougeManager>().StartConversation(dialouge);
    }
}
