using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour
{
    private Transform player;
    private P_Movement _pMovement;
    private float x;
    private float lerp;
    public float swordChangeSpeed;
    public float a;
    public float b;
    public float c;
    public float x_max;
    public float x_min;
    public float lerp_max;
    public float lerp_min;
    public float lerpVar;
    public float xVar;

    private bool goingUp;
    private bool lerpUp;

    private Animator _animator;
    private G_Vars _gVars;

    private TrailRenderer trailRenderer;
    

    private void Start()
    {
        trailRenderer = transform.Find("Trail").GetComponent<TrailRenderer>();
        player = GameObject.Find("Character").transform;
        _pMovement = player.GetComponent<P_Movement>();
        lerp = lerp_min;
        _animator = GetComponent<Animator>();
        _gVars = GameObject.Find("GameInfo").GetComponent<G_Vars>();
        if (_gVars.swordChosen == "Red")
        {
            _animator.Play("EspadaVermelha");
            trailRenderer.startColor = new Color(196, 0, 16);
            trailRenderer.endColor = new Color(196, 196, 196);
        }
        else if (_gVars.swordChosen == "White")
        {
            _animator.Play("EspadaBranca");
            trailRenderer.startColor = new Color(128, 128, 128);
            trailRenderer.endColor = new Color(255, 255, 255);
        }
        else if (_gVars.swordChosen == "Green")
        {
            _animator.Play("EspadaVerde");
            trailRenderer.startColor = new Color(16, 196, 0);
            trailRenderer.endColor = new Color(196, 196, 196);
        }
        else if (_gVars.swordChosen == "Pink")
        {
            _animator.Play("EspadaRosa");
            trailRenderer.startColor = new Color(98, 0, 98);
            trailRenderer.endColor = new Color(196, 196, 196);
        }
        else if (_gVars.swordChosen == "Blue")
        {
            _animator.Play("EspadaAzul");
            trailRenderer.startColor = new Color(0, 16, 196);
            trailRenderer.endColor = new Color(196, 196, 196);
        }
    }

    private void Update()
    {
        var y = a * Mathf.Pow(x, 2) + b * x + c;
        transform.position = new Vector3(x, y, 0) + Vector3.Lerp(transform.position, player.position, lerp);
        if (x > x_max)
        {
            goingUp = false;
        }
        else if (x < x_min)
        {
            goingUp = true;
        }

        if (lerp > lerp_max)
        {
            lerpUp = false;
        }
        else if (lerp < lerp_min)
        {
            lerpUp = true;
        }

        if (goingUp)
        {
            x += xVar;
        }
        else
        {
            x -= xVar;
        }

        if (lerpUp)
        {
            lerp += lerpVar;
        }
        else
        {
            lerp -= lerpVar;
        }
        
        if (_pMovement.jumping)
        {
            if(c < 5 && _pMovement.lastObjectTouched == "Roof")
            {
                c += swordChangeSpeed;
            }
            else if(c > -5  && _pMovement.lastObjectTouched == "Floor")
            {
                c -= swordChangeSpeed;
            }
        }
    }
}
