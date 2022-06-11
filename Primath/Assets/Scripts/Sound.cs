using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource soundAudio;
    [SerializeField] private AudioClip trueClip;
    [SerializeField] private AudioClip falseClip;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        soundAudio = this.GetComponent<AudioSource>();
        soundAudio.volume = 0.2f;
        soundAudio.loop = false;
        soundAudio.playOnAwake = false;
    }

    public float GetSoundVolume()
    {
        return soundAudio.volume;
    }

    public void ChangeSoundVolume(float vol)
    {
        soundAudio.volume = vol;
    }

    public void PlayTrueSound()
    {
        soundAudio.clip = trueClip;
        soundAudio.Play();
    }

    public void PlayFalseSound()
    {
        soundAudio.clip = falseClip;
        soundAudio.Play();
    }
}