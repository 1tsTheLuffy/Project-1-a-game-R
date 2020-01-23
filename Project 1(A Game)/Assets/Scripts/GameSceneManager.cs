using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene(01);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSceneFromCheckPoint()
    {
        SceneManager.LoadScene(01);
    }

    public void LoadNextGameTutorialScene()
    {
        SceneManager.LoadScene(04);
    }

    public void LoadNextGame()
    {
        SceneManager.LoadScene(03);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
