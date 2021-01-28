using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI_Spin : MonoBehaviour
{
    public Sprite selectedSprite;
    private Sprite normalSprite;
    public float initialRotationForce;
    public float initialChangeBoxTime;
    public float timeRolling;
    public float rotationSlow;
    public float changeBoxSlow;
    private Transform bingo;
    private bool bingoRoll;

    private List<Image> boxes = new List<Image>();

    public int index;

    private float timeCounter;
    private bool stop;

    private G_Vars _gVars;

    private int reveal;

    public float colorVariation;

    private float colorN;

    private Image blueSword;
    private Image pinkSword;
    private Image whiteSword;
    private Image greenSword;

    private float trueInitRot;
    private float trueInitBox;

    private GameObject buttonSpin;
    private TextMeshProUGUI priceText;
    private int unlockedSwords;
    public int price;
    private bool reduceCoins;
    private int reduceCont;

    public AudioClip bingoClip;
    public AudioClip prizeClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        buttonSpin = GameObject.Find("Spin");
        priceText = buttonSpin.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        _gVars = GameObject.Find("GameInfo").GetComponent<G_Vars>();
        bingo = GameObject.Find("Bingo").transform;
        foreach (Transform child in transform)
        {
            boxes.Add(child.GetComponent<Image>());
        }

        normalSprite = boxes[0].sprite;
        blueSword = boxes[0].transform.Find("Azul").GetComponent<Image>();
        pinkSword = boxes[1].transform.Find("Rosa").GetComponent<Image>();
        whiteSword = boxes[2].transform.Find("Branca").GetComponent<Image>();
        greenSword = boxes[3].transform.Find("Verde").GetComponent<Image>();
        trueInitRot = initialRotationForce;
        trueInitBox = initialChangeBoxTime;

        if (_gVars.blueSword)
        {
            blueSword.color = Color.white;
            unlockedSwords += 1;
        }
        if (_gVars.pinkSword)
        {
            pinkSword.color = Color.white;
            unlockedSwords += 1;
        }
        if (_gVars.whiteSword)
        {
            whiteSword.color = Color.white;
            unlockedSwords += 1;
        }
        if (_gVars.greenSword)
        {
            greenSword.color = Color.white;
            unlockedSwords += 1;
        }

        if(unlockedSwords == 4)
        {
            Destroy(buttonSpin);
        }

        for (int i = 0; i < unlockedSwords; i++)
        {
            price *= unlockedSwords + 1;
        }

        if (unlockedSwords == 1)
        {
            price *= price/3;
        }
        
        price += 6;
    }

    public void StartRolling()
    {
        if(_gVars.coins < price) return;
        reduceCoins = true;
        stop = false;
        timeCounter = 0;
        reveal = 0;
        index = 0;
        colorN = 0;
        initialChangeBoxTime = trueInitBox;
        _gVars.swordChosen = "";
        if(!_gVars.soundMuted)
        {
            _audioSource.clip = bingoClip;
            _audioSource.Play();
        }
        StartCoroutine("ChangeBox");
        bingoRoll = true;
        initialRotationForce = trueInitRot;
    }

    private void Update()
    {
        priceText.text = price.ToString();
        if (unlockedSwords == 4)
        {
            if(buttonSpin != null)
            {
                Destroy(buttonSpin);
            }
        }
        
        
        if (reduceCoins)
        {
            if (reduceCont < price)
            {
                _gVars.coins -= 1;
                reduceCont += 1;
            }
            else
            {
                reduceCoins = false;
                reduceCont = 0;
                unlockedSwords += 1;
                price *= unlockedSwords;
                if (unlockedSwords == 1)
                {
                    price *= price/3;
                }

                price += 6;
            }
        }
        if (_gVars.swordChosen != "" && _gVars.swordChosen != "Red")
        {
            if (_gVars.swordChosen == "Blue")
            {
                if(boxes[0].sprite != selectedSprite)
                {
                    foreach (var box in boxes)
                    {
                        if (box.sprite == selectedSprite)
                        {
                            box.sprite = normalSprite;
                        }
                    }
                    boxes[0].sprite = selectedSprite;
                }
            }
            else if (_gVars.swordChosen == "Pink")
            {
                foreach (var box in boxes)
                {
                    if (box.sprite == selectedSprite)
                    {
                        box.sprite = normalSprite;
                    }
                }
                if(boxes[1].sprite != selectedSprite)
                {
                    boxes[1].sprite = selectedSprite;
                }
            }
            else if (_gVars.swordChosen == "White")
            {
                foreach (var box in boxes)
                {
                    if (box.sprite == selectedSprite)
                    {
                        box.sprite = normalSprite;
                    }
                }
                if(boxes[2].sprite != selectedSprite)
                {
                    boxes[2].sprite = selectedSprite;
                }
            }
            else if (_gVars.swordChosen == "Green")
            {
                foreach (var box in boxes)
                {
                    if (box.sprite == selectedSprite)
                    {
                        box.sprite = normalSprite;
                    }
                }
                if(boxes[3].sprite != selectedSprite)
                {
                    boxes[3].sprite = selectedSprite;
                }
            }
        }
        if (initialChangeBoxTime > 2)
        {
            stop = true;
        }
        if (bingoRoll)
        {
            bingo.Rotate(0, 0, initialRotationForce);
            initialRotationForce -= initialRotationForce / rotationSlow;
        }
        
        if (timeRolling < timeCounter)
        {
            stop = true;
        }

        timeCounter += Time.deltaTime;
        
        
        if (reveal == 1)
        {
            if (blueSword.color.r < 255)
            {
                colorN += colorVariation;
                blueSword.color = new Color(colorN, colorN, colorN);
            }
        }
        else if (reveal == 2)
        {
            if (pinkSword.color.r < 255)
            {
                colorN += colorVariation;
                pinkSword.color = new Color(colorN, colorN, colorN);
            } 
        }
        else if (reveal == 3)
        {
            if (whiteSword.color.r < 255)
            {
                colorN += colorVariation;
                whiteSword.color = new Color(colorN, colorN, colorN);
            } 
        }
        else if (reveal == 4)
        {
            if (greenSword.color.r < 255)
            {
                colorN += colorVariation;
                greenSword.color = new Color(colorN, colorN, colorN);
            } 
        }
    }

    private IEnumerator ChangeBox()
    {
        if (stop)
        {
            if(reveal == 0)
            {
                if(!_gVars.soundMuted)
                {
                    _audioSource.clip = prizeClip;
                    _audioSource.Play();
                }
                if (index == 0)
                {
                    _gVars.blueSword = true;
                    _gVars.swordChosen = "Blue";
                    reveal = 1;
                    _gVars.SaveInfo();
                    _gVars.PrintVars();
                }
                else if (index == 1)
                {
                    _gVars.pinkSword = true;
                    _gVars.swordChosen = "Pink";
                    reveal = 2;
                    _gVars.SaveInfo();
                    _gVars.PrintVars();
                }
                else if (index == 2)
                {
                    _gVars.whiteSword = true;
                    _gVars.swordChosen = "White";
                    reveal = 3;
                    _gVars.SaveInfo();
                    _gVars.PrintVars();
                }
                else if (index == 3)
                {
                    _gVars.greenSword = true;
                    _gVars.swordChosen = "Green";
                    reveal = 4;
                    _gVars.SaveInfo();
                    _gVars.PrintVars();
                }
            }
        }
        else
        {
            if (index == 3 && !_gVars.greenSword && _gVars.blueSword && _gVars.pinkSword && _gVars.whiteSword)
            {
                
            }
            else
            {
                index += 1;
            }
            if (index == 0 && _gVars.blueSword)
            {
                index += 1;
                if (_gVars.pinkSword)
                {
                    index += 1;
                    if (_gVars.whiteSword)
                    {
                        index += 1;
                    }
                }
            }
            if (index == 1 && _gVars.pinkSword)
            {
                index += 1;
            }
            if (index == 2 && _gVars.whiteSword)
            {
                index += 1;
            }
            if (index == 3 && _gVars.greenSword)
            {
                if (!_gVars.blueSword)
                {
                    index = 0;
                }
                else if(!_gVars.pinkSword)
                {
                    index = 1;
                }
                else if (!_gVars.whiteSword)
                {
                    index = 2;
                }
            }

            if (index > 3)
            {
                if (!_gVars.blueSword)
                {
                    index = 0;
                }
                else if(!_gVars.pinkSword)
                {
                    index = 1;
                }
                else if (!_gVars.whiteSword)
                {
                    index = 2;
                }
            }
            boxes[index].sprite = selectedSprite;
            foreach (var box in boxes)
            {
                if (box.sprite == selectedSprite)
                {
                    if (box != boxes[index])
                    {
                        box.sprite = normalSprite;
                    }
                }
            }

            yield return new WaitForSeconds(initialChangeBoxTime);
            initialChangeBoxTime += changeBoxSlow + Random.Range(0.05f, 0.25f);
            StartCoroutine("ChangeBox");
        }
        
    }
}
