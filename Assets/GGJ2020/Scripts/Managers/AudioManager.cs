using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<MyAudio> audios = new List<MyAudio>();

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
}
