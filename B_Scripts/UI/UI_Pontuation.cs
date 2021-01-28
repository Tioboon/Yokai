using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Pontuation : MonoBehaviour
{
    private Transform player;
    private P_Comportament _pComportament;
    private TextMeshProUGUI text;

    private void Start()
    {
        player = GameObject.Find("Character").transform;
        _pComportament = player.GetComponent<P_Comportament>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = _pComportament.pontuation.ToString();
    }
}
