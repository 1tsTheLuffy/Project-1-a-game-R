﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }
}
