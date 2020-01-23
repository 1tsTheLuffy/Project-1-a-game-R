using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadNextScene()
    {
        StartCoroutine(loadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadScene(int buildIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(buildIndex);
    }
}
