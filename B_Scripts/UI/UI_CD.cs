using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CD : MonoBehaviour
{
    private P_Input _pInput;
    private Image image;
    private float maxCD;

    private void Start()
    {
      _pInput = GameObject.Find("Character").GetComponent<P_Input>();
      image = GetComponent<Image>();
      maxCD = _pInput.cooldown;
    }

    private void Update()
    {
        maxCD = _pInput.cooldown;
        var coeficient = 1 / maxCD;
        image.fillAmount = _pInput.keyCD * coeficient;
    }
}
