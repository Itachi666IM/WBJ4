using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuBGM;
    public AudioClip gameBGM;
    public AudioClip bossBGM;

    private AudioSource myAudio;

    [SerializeField] Slider volumeSlider;
    private void ManageSingleton()
    {
        int instance = FindObjectsOfType<AudioManager>().Length;
        if(instance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Awake()
    {
        ManageSingleton();
        myAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            myAudio.clip = gameBGM;
        }
        else if(SceneManager.GetActiveScene().name == "Boss")
        {
            myAudio.clip = bossBGM;
        }
        else
        {
            myAudio.clip = menuBGM;
        }
        if(!myAudio.isPlaying)
        {
            myAudio.Play();
        }
    }

    public void SetVolume()
    {
        myAudio.volume = volumeSlider.value;
    }
}
