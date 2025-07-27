using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    SFX sfx;
    public AudioClip teleportSound;
    private void Start()
    {
        sfx = FindObjectOfType<SFX>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sfx.PlayAnySound(teleportSound);
            Invoke(nameof(LoadNextLevel), 1.5f);
        }    
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
