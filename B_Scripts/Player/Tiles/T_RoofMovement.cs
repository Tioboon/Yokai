using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_RoofMovement : MonoBehaviour
{
    public float speed;
    private P_Movement _pMovement;

    private void Start()
    {
        _pMovement = GameObject.Find("Character").GetComponent<P_Movement>();
    }

    void Update()
    {
        if (_pMovement._pComportament.dead)
        {
            
        }
        else
        {
            if (_pMovement.moving)
            {
                transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            }
            else if (_pMovement.jumping)
            {
                transform.position += new Vector3(-speed * 2, 0, 0) * Time.deltaTime;
            }
        }
    }
}
