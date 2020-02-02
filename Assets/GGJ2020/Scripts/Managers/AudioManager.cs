using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clipBoss;
    public AudioClip clipLevel;
    public List<MyAudio> audios = new List<MyAudio>();

    private void Start()
    {
        SwitchToLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchToBoss();
        }
    }

    [Serializable]
    public class MyAudio
    {
        public string name;
        public AudioClip clip;
    }

    public void PlaySound(string name)
    {
        foreach(MyAudio a in audios){
            if(name == a.name)
            {
                audioSource.PlayOneShot(a.clip);
                break;
            }
        }
    }

    public void SwitchToBoss()
    {
        audioSource.clip = clipBoss;
        audioSource.Play();
    }

    public void SwitchToLevel()
    {
        audioSource.clip = clipLevel;
        audioSource.Play();
    }
}
