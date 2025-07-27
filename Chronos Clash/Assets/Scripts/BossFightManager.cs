using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightManager : MonoBehaviour
{
    public GameObject winScreen;
    private Boss boss;
    SFX sfx;
    public AudioClip winSound;
    bool once;
    private void Start()
    {
        boss = FindObjectOfType<Boss>();
        sfx = FindObjectOfType<SFX>();
    }

    private void Update()
    {
        if(boss.health <=0)
        {
            Time.timeScale = 1.0f;
            winScreen.SetActive(true);
            if(!once)
            {
                sfx.PlayAnySound(winSound);
                once = true;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
