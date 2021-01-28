using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Sounds : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public AudioClip dash;
    public AudioClip dying;
    public AudioClip running;
    public AudioClip dropping;

    private G_Vars gVars;
    

    private void Start()
    {
        gVars = GameObject.Find("GameInfo").GetComponent<G_Vars>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = running;
        _audioSource.Play();
        _audioSource.loop = true;
    }

    public void RunningSound()
    {
        if(gVars.soundMuted) return;
        _audioSource.clip = running;
        _audioSource.Play();
        _audioSource.loop = true;
    }

    public void DyingSound()
    {
        if(gVars.soundMuted) return;
        _audioSource.clip = dying;
        _audioSource.Play();
        _audioSource.loop = false;
    }

    public void DashingSound()
    {
        if(gVars.soundMuted) return;
        _audioSource.clip = dash;
        _audioSource.Play();
        _audioSource.loop = false;
    }

    public void DroppingSound()
    {
        if(gVars.soundMuted) return;
        _audioSource.clip = dropping;
        _audioSource.Play();
        _audioSource.loop = false;
    }
}
