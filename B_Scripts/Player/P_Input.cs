using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Input : MonoBehaviour
{
    public float cooldown;
    private float initCoolDown;
    private P_Movement _pMovement;
    private C_FollowPlayer _followPlayer;
    private Animator _animator;
    public float keyCD;
    private bool firstTime;
    void Start()
    {
        _followPlayer = GameObject.Find("Camera").GetComponent<C_FollowPlayer>();
        _pMovement = GetComponent<P_Movement>();
        _animator = GetComponent<Animator>();
        initCoolDown = cooldown;
        cooldown = _pMovement.runFasterSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        keyCD += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
        {
            if(_pMovement._pComportament.dead) return;
            if(!_pMovement.correu) return;
            if(keyCD < cooldown) return;
            if (_pMovement.moving)
            {
                _animator.Play("Squat");
                _pMovement.StopMove(true);
                _pMovement.moving = false;
            }
            else
            {
                _pMovement.Dash();
                _pMovement.jumping = true;
                if (_followPlayer.inverted)
                {
                    _followPlayer.inverted = false;
                }
                else
                {
                    _followPlayer.inverted = true;
                }

                keyCD = 0;
                if (!firstTime)
                {
                    cooldown = initCoolDown;
                    firstTime = true;
                }
            }
        }
    }
}
