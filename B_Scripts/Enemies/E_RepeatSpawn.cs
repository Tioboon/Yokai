using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_RepeatSpawn : MonoBehaviour
{
    public float timeAlive;
    private Transform camera;

    private void Start()
    {
        camera = GameObject.Find("Camera").transform;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == transform.parent.name)
        {
            var otherRepeatSpawn = other.transform.Find("SpawnerHandler").GetComponent<E_RepeatSpawn>();
            if (other.transform.position.x < camera.position.x + 50)
            {
                print("1");
                if (transform.position.x > camera.position.x + 50)
                {
                    print("2");
                    Destroy(gameObject);
                }
            }
            else if (otherRepeatSpawn.timeAlive < timeAlive)
            {
                print(other.name + " Destruiu");
                Destroy(other.gameObject);
            }
        }
    }
}
