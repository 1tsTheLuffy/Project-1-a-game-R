using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadNextScene(int index)
    {
        StartCoroutine(loadScene(index));
    }

    IEnumerator loadScene(int buildIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
