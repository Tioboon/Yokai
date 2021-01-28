using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class C_Movement : MonoBehaviour
{
    public float velocityStopped = 2.5f;
    public float changeSpeedRate;
    [FormerlySerializedAs("velocityOnCamera")] public float velocityOnJump = 5;
    public float speedVariation;
    private Rigidbody2D rb;
    private P_Movement _pMovement;
    public float velocity;
    public float velocityAbovePlayerDivisor;
    private C_FollowPlayer _cFollowPlayer;

    private void Start()
    {
        _cFollowPlayer = GetComponent<C_FollowPlayer>();
        _pMovement = GameObject.Find("Character").GetComponent<P_Movement>();
        rb = GetComponent<Rigidbody2D>();
        velocity = _pMovement.actualVelocity + _pMovement.actualVelocity / velocityAbovePlayerDivisor;
        InvokeRepeating("ChangeSpeed", 0, changeSpeedRate);
        
    }

    private void Update()
    {
        if (_pMovement._pComportament.dead)
        {
            rb.velocity = Vector2.zero;
            _cFollowPlayer.enabled = true;
            GetComponent<C_Movement>().enabled = false;
        }
        else
        {
            if (_pMovement.jumping)
            {
                rb.velocity = new Vector2(velocity * velocityOnJump, 0);
            }
            else if (!_pMovement.moving)
            {
                rb.velocity = new Vector2(velocity * velocityStopped, 0);
            }
            else
            {
                rb.velocity = new Vector2(velocity, 0);
            }
        }
    }

    void ChangeSpeed()
    {
        velocity += speedVariation;
    }
}
