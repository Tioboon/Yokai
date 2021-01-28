using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PA_ArrowMovement : MonoBehaviour
{
    public float minAngle;
    public float maxAngle;
    public float invertedMinAngle;
    public float invertedMaxAngle;
    public float angleTaxVar;
    public float angleSpeedVar;
    public float angleVariation;
    private bool goingUp;
    private P_Movement _pMovement;

    private void OnEnable()
    {
        _pMovement = transform.parent.GetComponent<P_Movement>();
        if (_pMovement.onFloor)
        {
            float angle = Random.Range(0, 1);
            if(angle == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, minAngle + 1);
                goingUp = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, maxAngle - 1);
                goingUp = false;
            }
        }
        else
        {
            float angle = Random.Range(0, 1);
            if(angle == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, invertedMinAngle + 1);
                goingUp = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, invertedMaxAngle - 1);
                goingUp = false;
            }
        }
        StartCoroutine(RotateInBounds());
    }
    void Update()
    {
        if(_pMovement.onFloor)
        {
            if (transform.eulerAngles.z > maxAngle)
            {
                angleVariation = -angleVariation;
                transform.eulerAngles = new Vector3(0, 0, maxAngle - 1);
            }
            else if (transform.eulerAngles.z < minAngle)
            {
                angleVariation = -angleVariation;
                transform.eulerAngles = new Vector3(0, 0, minAngle + 1);
            }
        }
        else
        {
            if (transform.eulerAngles.z < invertedMinAngle)
            {
                angleVariation = -angleVariation;
                transform.eulerAngles = new Vector3(0, 0, invertedMinAngle + 1);
            }
            else if (transform.eulerAngles.z > invertedMaxAngle)
            {
                angleVariation = -angleVariation;
                transform.eulerAngles = new Vector3(0, 0, invertedMaxAngle - 1);
            }
        }
    }

    private IEnumerator RotateInBounds()
    {
        transform.Rotate(0, 0, angleVariation * Time.deltaTime);
        yield return new WaitForSeconds(angleSpeedVar);
        angleVariation += angleTaxVar;
        StartCoroutine(RotateInBounds());
    }
}
