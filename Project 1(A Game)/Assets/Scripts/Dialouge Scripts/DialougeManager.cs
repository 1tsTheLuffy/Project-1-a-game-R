using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManager : MonoBehaviour
{
    private Queue<string> Sentences;

    public TextMeshProUGUI DialougeText;

    public Animator animator;

    private void Start()
    {
        Sentences = new Queue<string>();
    }

    public void StartConversation(Dialouge dialouge)
    {
        Sentences.Clear();
        animator.SetBool("IsOpen", true);
        foreach (string sentence in dialouge.sentences)
        {
            Sentences.Enqueue(sentence);
        }
        DisplayNextDialouge();
    }

    public void DisplayNextDialouge()
    {
        if(Sentences.Count == 0)
        {
            EndOfConversation();
            return;
        }

        string sentence =  Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type(sentence));
    }

    IEnumerator Type(string sentence)
    {
        DialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialougeText.text += letter;
            yield return null;
        }
    }

    private void EndOfConversation()
    {
        animator.SetBool("IsOpen", false);
    }
}
