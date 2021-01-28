using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Tentacles : MonoBehaviour
{
    public float timeToSpawnButtons;
    public float alphaVar;
    private P_Comportament _pComportament;
    private GameObject tentacles;
    private Animator _animator;
    private bool spawned;
    private float timeCounter;
    private float animTime;
    private bool firstTime;
    private Transform fade;
    private Image fadeImage;
    private GameObject fadeScreen;
    private Transform player;
    private GameObject buttons;
    private bool activated;
    private AudioSource _audioSource;
    private G_Vars gVars;
    private void Start()
    {
        gVars = GameObject.Find("GameInfo").GetComponent<G_Vars>();
        _pComportament = GameObject.Find("Character").GetComponent<P_Comportament>();
        tentacles = transform.Find("Canvas").Find("Tentaculos").gameObject;
        _animator = tentacles.GetComponent<Animator>();
        fadeScreen = GameObject.Find("FadeScreen");
        fade = fadeScreen.transform.Find("BG");
        fadeImage = fade.GetComponent<Image>();
        player = GameObject.Find("Character").transform;
        buttons = fadeScreen.transform.Find("Buttons").gameObject;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(activated) return;
        if (_pComportament.dead)
        {
            if (!spawned)
            {
                tentacles.transform.position = new Vector3(tentacles.transform.position.x, player.position.y);
                tentacles.SetActive(true);
                spawned = true;
            }
            else
            {
                if (timeToSpawnButtons < timeCounter)
                {
                    if(gVars.soundMuted)
                    {
                        _audioSource.Play();
                    }
                    fadeImage.color = Color.black;
                    buttons.SetActive(true);
                    activated = true;
                }
                else
                {
                    timeCounter += Time.deltaTime;
                    var tempColor = fadeImage.color;
                    tempColor.a += alphaVar;
                    fadeImage.color += tempColor;
                }
            }
        }
    }
}
