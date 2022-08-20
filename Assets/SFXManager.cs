using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public AudioClip[] hitClips;
    public AudioClip[] dieClips;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playHitClip()
    {
        audioSource.PlayOneShot(hitClips[Random.Range(0, hitClips.Length)]);
    }
    public void playDieClip()
    {
        audioSource.PlayOneShot(dieClips[Random.Range(0, dieClips.Length)]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
