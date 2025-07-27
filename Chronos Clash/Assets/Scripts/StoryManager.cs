using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public string[] dialogs;
    int index;
    string currentDialogue;
    public GameObject continueButton;
    public GameObject startGameButton;
    public float typingSpeed;
    public GameObject chronos;
    bool once;
    public GameObject dialogBox;
    SFX sfx;
    public AudioClip typingSound;

    private void Start()
    {
        sfx = FindObjectOfType<SFX>();
        dialogueText.text = "";
        currentDialogue = dialogs[index];
        StartCoroutine(WriteSentence());
    }

    IEnumerator WriteSentence()
    {
        foreach(char c in currentDialogue.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        sfx.PlayAnySound(typingSound);
        yield return new WaitForSeconds(typingSpeed);
        continueButton.SetActive(true);
    }

    public void NewSentence()
    {
        dialogueText.text = "";
        continueButton.SetActive(false);
        if(index+1 < dialogs.Length)
        {
            index++;
            currentDialogue = dialogs[index];
            StartCoroutine(WriteSentence());
        }
        else
        {
            dialogueText.text = "";
            dialogBox.SetActive(false);
            startGameButton.SetActive(true);
        }
    }

    private void Update()
    {
        if(index>6)
        {
            if(!once)
            {
                chronos.SetActive(true);
                once = true;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
