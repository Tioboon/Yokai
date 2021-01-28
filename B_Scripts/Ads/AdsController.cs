using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    private static AdsController adsInstance;
    
    public string gameId = "3974431";
    public bool testMode = true;
    
    public bool adSeen;
    
    public int deadTimesToAd;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (adsInstance == null)
        {
            adsInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        Advertisement.Initialize (gameId, testMode);
        deadTimesToAd = PlayerPrefs.GetInt("Ads");
    }
}
