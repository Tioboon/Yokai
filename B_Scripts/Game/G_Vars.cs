using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Vars : MonoBehaviour
{
    private static G_Vars varsInstance;

    public int coins;
    public int highScore;
    public bool blueSword;
    public bool whiteSword;
    public bool pinkSword;
    public bool greenSword;
    public string swordChosen;
    public bool initialized;
    public bool musicMuted;
    public bool soundMuted;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (varsInstance == null)
        {
            varsInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        coins = PlayerPrefs.GetInt("Coins");
        highScore = PlayerPrefs.GetInt("HighScore");
        if (PlayerPrefs.GetInt("BlueSword") == 1)
        {
            blueSword = true;
        }
        else
        {
            blueSword = false;
        }
        if (PlayerPrefs.GetInt("PinkSword") == 1)
        {
            pinkSword = true;
        }
        else
        {
            pinkSword = false;
        }
        if (PlayerPrefs.GetInt("WhiteSword") == 1)
        {
            whiteSword = true;
        }
        else
        {
            whiteSword = false;
        }
        if (PlayerPrefs.GetInt("GreenSword") == 1)
        {
            greenSword = true;
        }
        else
        {
            greenSword = false;
        }

        Debug.Log(PlayerPrefs.GetInt("Initialized"));
        if (PlayerPrefs.GetInt("Initialized") == 1)
        {
            initialized = true;
        }
        else
        {
            initialized = false;
        }
        if (!initialized)
        {
            swordChosen = PlayerPrefs.GetString("Sword");
            PlayerPrefs.SetInt("Initialized", 1);
            swordChosen = "Red";
            initialized = true;
        }
        else
        {
            swordChosen = PlayerPrefs.GetString("Sword");
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicMuted = true;
        }
        else
        {
            musicMuted = false;
        }
        
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundMuted = true;
        }
        else
        {
            soundMuted = false;
        }
    }

    public void SetRedSword()
    {
        swordChosen = "Red";
    }
    public void SetBlueSword()
    {
        if(!blueSword) return;
        swordChosen = "Blue";
    }
    public void SetPinkSword()
    {
        if(!pinkSword) return;
        swordChosen = "Pink";
    }
    public void SetWhiteSword()
    {
        if(!whiteSword) return;
        swordChosen = "White";
    }
    public void SetGreenSword()
    {
        if(!greenSword) return;
        swordChosen = "Green";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Reinicializado Prefs");
            PlayerPrefs.DeleteAll();
        }
    }

    public void PrintVars()
    {
        Debug.Log(coins + " - coins");
        Debug.Log(highScore + " - score");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SaveInfo();
    }

    public void SaveInfo()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("HighScore", highScore);
        if(blueSword)
        {
            PlayerPrefs.SetInt("BlueSword", 1);
        }
        else
        {
            PlayerPrefs.SetInt("BlueSword", 0);
        }
        if(pinkSword)
        {
            PlayerPrefs.SetInt("PinkSword", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PinkSword", 0);
        }
        if(greenSword)
        {
            PlayerPrefs.SetInt("GreenSword", 1);
        }
        else
        {
            PlayerPrefs.SetInt("GreenSword", 0);
        }
        if(whiteSword)
        {
            PlayerPrefs.SetInt("WhiteSword", 1);
        }
        else
        {
            PlayerPrefs.SetInt("WhiteSword", 0);
        }
        PlayerPrefs.SetString("Sword", swordChosen);
        print(PlayerPrefs.GetString("Sword"));
        if (initialized)
        {
            PlayerPrefs.SetInt("Initialized", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Initialized", 0);
        }
        if(soundMuted)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        if(musicMuted)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }
    
}
