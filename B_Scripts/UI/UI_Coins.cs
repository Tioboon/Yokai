using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    private G_Vars gVars;
    private TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        gVars = GameObject.Find("GameInfo").GetComponent<G_Vars>();
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMeshProUGUI.text = gVars.coins.ToString();
    }
}
