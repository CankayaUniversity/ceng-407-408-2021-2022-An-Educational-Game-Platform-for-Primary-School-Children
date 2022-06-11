using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicAudio;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        musicAudio = this.GetComponent<AudioSource>();
        musicAudio.volume = 0.2f;
    }

    public float GetMusicVolume()
    {
        return musicAudio.volume;
    }

    public void ChangeMusicVolume(float vol)
    {
        musicAudio.volume = vol;
    }
}
