using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioClip buildClip;
    public AudioClip battleClip;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void startBuild()
    {
        source.clip = buildClip;
        source.Play();
    }
    public void startBattle()
    {
        source.clip = battleClip;
        source.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
