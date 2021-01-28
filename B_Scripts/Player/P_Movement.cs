using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public float velocityIncreaseCadence;
    public float velocityVar;
    public float actualVelocity;
    public float jumpForce;
    public float scaleVar;
    public bool moving;
    public bool jumping;
    public bool onFloor;
    public float runFasterSeconds;
    public float howMuchFaster;
    private float fasterCount;
    private Rigidbody2D rb;
    private GameObject arrow;
    public string lastObjectTouched;
    private Animator _animator;
    public P_Comportament _pComportament;
    private bool doingDeadThings;
    private float initialGravity;
    private Vector2 initScale;
    public bool correu;

    private P_Sounds _pSounds;

    private void Start()
    {
        initScale = transform.localScale;
        _animator = GetComponent<Animator>();
        lastObjectTouched = "Floor";
        rb = GetComponent<Rigidbody2D>();
        initialGravity = rb.gravityScale;
        arrow = transform.Find("Arrow").gameObject;
        _pComportament = GetComponent<P_Comportament>();
        _pSounds = GetComponent<P_Sounds>();
        MoveForward();
        InvokeRepeating("IncreaseVelocity", 0, velocityIncreaseCadence);
    }

    public void MoveForward()
    {
        rb.velocity = new Vector2(actualVelocity, 0);
        moving = true;
    }

    public void StopMove(bool activeArrow)
    {
        if(!correu) return;
        if(doingDeadThings) return;
        rb.velocity = new Vector2(0, 0);
        if(activeArrow)
        {
            arrow.SetActive(true);
        }
    }


    public void Dash()
    {
        if(!correu) return;
        if(doingDeadThings) return;
        _pSounds.DashingSound();
        Vector2 arrowDirection = arrow.transform.right;
        arrow.SetActive(false);
        Vector2 jumpVec = arrowDirection * jumpForce;
        rb.AddForce(jumpVec);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(_pComportament.dead) return;
        if (jumping)
        {
            if (other.transform.CompareTag("Lantern"))
            {
                Destroy(other.gameObject);
                _pComportament.lanternNumber += 1;
            }
            
            if (other.transform.CompareTag("Floor"))
            {
                if(lastObjectTouched == "Roof")
                {
                    _pSounds.DroppingSound();
                    jumping = false;
                    StopMove(false);
                    MoveForward();
                    if (onFloor)
                    {
                        onFloor = false;
                    }
                    else
                    {
                        onFloor = true;
                    }

                    rb.gravityScale = -rb.gravityScale;
                    lastObjectTouched = other.transform.tag;
                    _animator.Play("Running");
                    _pSounds.RunningSound();
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
            }
            else if (other.transform.CompareTag("Roof"))
            {
                if(lastObjectTouched == "Floor")
                {
                    _pSounds.DroppingSound();
                    jumping = false;
                    StopMove(false);
                    MoveForward();
                    if (onFloor)
                    {
                        onFloor = false;
                    }
                    else
                    {
                        onFloor = true;
                    }

                    rb.gravityScale = -rb.gravityScale;
                    lastObjectTouched = other.transform.tag;
                    _animator.Play("Running");
                    _pSounds.RunningSound();
                    transform.localScale = new Vector3(1.5f, -1.5f, 1);
                }
            }
        }
    }

    private void IncreaseVelocity()
    {
        actualVelocity += velocityVar;
    }

    private void Update()
    {
        if(fasterCount < runFasterSeconds) 
        {
            fasterCount += Time.deltaTime;
            rb.velocity = new Vector2(actualVelocity * howMuchFaster, 0);
        }
        else
        {
            if(!correu)
            {
                correu = true;
                MoveForward();
            }
        }
        
        
        if (_pComportament.dead && !doingDeadThings)
        {
            StopMove(false);
            _animator.Play("Dying");
            _pSounds.DyingSound();
            if (rb.gravityScale < 0)
            {
                rb.gravityScale = initialGravity;
            }
            Destroy(arrow);
            doingDeadThings = true;
        }
        else if (doingDeadThings)
        {
            if (transform.localScale.y < 1.5)
            {
                transform.localScale += new Vector3(0, scaleVar, 0);
            }
            else
            {
                transform.localScale = initScale;
            }
        }
        else
        {
            if (jumping)
            {
                if (transform.lossyScale.y < 1.5f && lastObjectTouched == "Roof")
                {
                    transform.localScale += new Vector3(0, scaleVar, 0);
                }
                else if (transform.lossyScale.y > -1.5f && lastObjectTouched == "Floor")
                {
                    transform.localScale -= new Vector3(0, scaleVar, 0);
                }
            }

            if (moving && rb.velocity.x == 0)
            {
                MoveForward();
            }

            if (_pComportament.dead && rb.velocity.x > 0)
            {
                StopMove(false);
            }
        }
    }
}
