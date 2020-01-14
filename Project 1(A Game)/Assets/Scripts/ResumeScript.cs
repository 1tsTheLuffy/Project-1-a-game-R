using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeScript : MonoBehaviour
{
    public void LoadSceneFromCheckPoint()
    {
        SceneManager.LoadScene(0);
    }
}
