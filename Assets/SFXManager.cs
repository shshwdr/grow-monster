using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public AudioClip[] hitClips;
    public AudioClip[] dieClips;

    public AudioClip grow;
    public AudioClip eat;

    public AudioClip[] humanHitClips;
    public AudioClip[] monsterGetHitClips;
    public AudioClip monsterDieClip;

    public AudioClip humanWinClip;
    public AudioClip monsterWinClip;


    public AudioClip castClip;
    public AudioClip healClip;
    public AudioClip collectClip;

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
    public void playGrowClip()
    {
        audioSource.PlayOneShot(grow);
    }


    public void playHumanAttackClip()
    {
        audioSource.PlayOneShot(humanHitClips[Random.Range(0, humanHitClips.Length)]);
    }
    public void playMonsterHurtClip()
    {
        audioSource.PlayOneShot(monsterGetHitClips[Random.Range(0, monsterGetHitClips.Length)]);
    }
    public void playMonsterDieClip()
    {
        audioSource.PlayOneShot(monsterDieClip);
    }
    public void playHumanWinClip()
    {
        audioSource.PlayOneShot(humanWinClip);
    }
    public void playMonsterWinClip()
    {
        audioSource.PlayOneShot(monsterWinClip);
    }
    public void playcastClip()
    {
        audioSource.PlayOneShot(castClip);
    }
    public void playhealClip()
    {
        audioSource.PlayOneShot(healClip);
    }
    public void playcollectClip()
    {
        audioSource.PlayOneShot(collectClip);
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
