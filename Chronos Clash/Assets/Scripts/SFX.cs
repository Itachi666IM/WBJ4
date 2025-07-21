using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX : MonoBehaviour
{
    private AudioSource myAudio;

    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        int instance = FindObjectsOfType<SFX>().Length;
        if(instance>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayAnySound(AudioClip clip)
    {
        myAudio.PlayOneShot(clip);
    }

    public void SetVolume()
    {
        myAudio.volume = volumeSlider.value;
    }
}
