using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Tengu : MonoBehaviour
{
    public float scaleVelocity;
    public float velocity;
    public float timeToChangeDirection;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 initScale;
    private P_Comportament _pComportament;
    private void Start()
    {
        initScale = transform.localScale;
        player = GameObject.Find("Character").transform;
        rb = GetComponent<Rigidbody2D>();
        _pComportament = player.GetComponent<P_Comportament>();
        InvokeRepeating("FollowPlayer", 0, timeToChangeDirection);
        
    }

    private void FollowPlayer()
    {
        if (_pComportament.dead)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = Vector3.zero;
                if(transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-initScale.x, initScale.y, 0);
                }
                else
                {
                    transform.localScale = initScale;
                }
            }
            return;
        };
        var distance = player.position - transform.position;
        distance.Normalize();
        rb.velocity = distance * velocity;
        if (distance.x > 0 && transform.localScale.x > -initScale.x)
        {
            transform.localScale -= new Vector3(scaleVelocity, 0, 0);
        }
        else if (distance.x < 0 && transform.localScale.x < initScale.x)
        {
            transform.localScale += new Vector3(scaleVelocity, 0, 0);
        }
    }
}
