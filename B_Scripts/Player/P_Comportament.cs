using System;
using System.Collections;
using System.Collections.Generic;
using B_Scripts.Ads;
using UnityEngine;

public class P_Comportament : MonoBehaviour
{
    public bool dead;
    public int lanternNumber;
    public int pontuation;
    private P_Movement _pMovement;
    private T_FloorMovement _floorMovement;
    private float floorPontuation;
    
    
    private Ads_Inter _adsInter;
    private Ads_Rewarded _adsRewarded;
    private AdsController _adsController;
    public int timesDeadToSeeAd = 2;

    private G_Vars gVars;

    private void Start()
    {
        gVars = GameObject.Find("GameInfo").GetComponent<G_Vars>();
        _pMovement = GetComponent<P_Movement>();
        _floorMovement = GameObject.Find("FloorGenerator").transform.Find("Floor").GetComponent<T_FloorMovement>();
        var adsController = GameObject.Find("AdsController");
        _adsInter = adsController.GetComponent<Ads_Inter>();
        _adsRewarded = adsController.GetComponent<Ads_Rewarded>();
        _adsController = adsController.GetComponent<AdsController>();
    }

    private void Update()
    {
        if(dead) return;
        if (_pMovement.jumping)
        {
            floorPontuation += _floorMovement.speed * _floorMovement.velocityVar;  
        }
        else if (_pMovement.moving)
        {
            floorPontuation += _floorMovement.speed;   
        }
        pontuation = Mathf.RoundToInt(transform.position.x) + 30 + Mathf.RoundToInt(floorPontuation/10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Guardian")
        {
            var guardAnim = other.GetComponent<Animator>();
            guardAnim.Play("G_Puto");
            if(!gVars.soundMuted)
            {
                var guardSound = other.GetComponent<AudioSource>();
                guardSound.Play();
            }
        }

        if (other.name == "BackWall" && !dead)
        {
            gVars.SaveInfo();
            gVars.PrintVars();
            dead = true;
            if (pontuation > gVars.highScore)
            {
                gVars.highScore = pontuation;
            }

            gVars.coins += lanternNumber;
            if(!_adsController.adSeen)
            {
                if(_adsController.deadTimesToAd <= 0)
                {
                    _adsInter.ShowInterstitialAd();
                    _adsController.deadTimesToAd = timesDeadToSeeAd;
                    PlayerPrefs.SetInt("Ads", _adsController.deadTimesToAd);
                }
                else
                {
                    _adsController.deadTimesToAd -= 0;
                    PlayerPrefs.SetInt("Ads", _adsController.deadTimesToAd);
                }
            }
            else
            {
                _adsController.deadTimesToAd -= 0;
            }
        }
        
        if (other.CompareTag("Enemy") && !dead)
        {
            gVars.SaveInfo();
            gVars.PrintVars();
            dead = true;
            if (pontuation > gVars.highScore)
            {
                gVars.highScore = pontuation;
            }

            gVars.coins += lanternNumber;
            if(!_adsController.adSeen)
            {
                if(_adsController.deadTimesToAd <= 0)
                {
                    _adsInter.ShowInterstitialAd();
                    _adsController.deadTimesToAd = timesDeadToSeeAd;
                    PlayerPrefs.SetInt("Ads", _adsController.deadTimesToAd);
                }
                else
                {
                    _adsController.deadTimesToAd -= 0;
                    PlayerPrefs.SetInt("Ads", _adsController.deadTimesToAd);
                }
            }
            else
            {
                _adsController.deadTimesToAd -= 0;
            }
            
        }

        if (_pMovement.jumping)
        {
            if (other.transform.CompareTag("Lantern"))
            {
                if(!gVars.soundMuted)
                {
                    AudioSource audioFromObj = other.GetComponent<AudioSource>();
                    audioFromObj.Play();
                }
                Animator animFromObj = other.GetComponent<Animator>();
                animFromObj.Play("L_Die");
                var timeOfAnimation = animFromObj.GetCurrentAnimatorStateInfo(0).length;
                print(timeOfAnimation);
                StartCoroutine(DestroyWithDelay(0.8f, other.gameObject));
                pontuation += 100;
                lanternNumber += 1;
            }
        }
    }

    private IEnumerator DestroyWithDelay(float seconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
