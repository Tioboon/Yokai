using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Destroy : MonoBehaviour
{
    private GameObject camera;

    private void Start()
    {
        camera = GameObject.Find("Camera");
    }

    private void Update()
    {
        if (transform.position.x < camera.transform.position.x - 60)
        {
            Destroy(this);
        }
    }
}
