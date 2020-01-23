using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    public void LoadPlayScene()
    {
        levelLoader.LoadNextScene(01);
       // SceneManager.LoadScene(01);
    }

    public void LoadMainMenuScene()
    {
        levelLoader.LoadNextScene(00);
       // SceneManager.LoadScene(0);
    }

    public void LoadSceneFromCheckPoint()
    {
        levelLoader.LoadNextScene(01);
        //SceneManager.LoadScene(01);
    }

    public void LoadNextGameTutorialScene()
    {
        levelLoader.LoadNextScene(04);
       // SceneManager.LoadScene(04);
    }

    public void LoadNextGame()
    {
        levelLoader.LoadNextScene(03);
       // SceneManager.LoadScene(03);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
