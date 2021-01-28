using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Points : MonoBehaviour
{
    private TextMeshProUGUI text;
    private P_Comportament _pComportament;

    private void Start()
    {
        _pComportament = GameObject.Find("Character").GetComponent<P_Comportament>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = _pComportament.lanternNumber.ToString();
    }
}
