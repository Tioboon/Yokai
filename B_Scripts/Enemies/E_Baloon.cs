using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Baloon : MonoBehaviour
{
    public float vel;
    public float m_Height;
    private bool goingUp;
    private Vector2 initialPos;
    private float initVel;
    
    void Start()
    {
        initVel = vel;
        vel = initVel - initVel/5;
        initialPos = transform.position;
    }
    
    void Update()
    {
        UpAndDown(m_Height, vel);
    }

    private void UpAndDown(float maxheight, float velocity)
    {
        var vecY = transform.position.y - initialPos.y;
        if (vecY > maxheight)
        {
            goingUp = false;
            vel = initVel - initVel/10;
        }

        if (vecY <= 0)
        {
            goingUp = true;
            vel = initVel - initVel/10;
        }
        
        if(goingUp)
        {
            if (vel < initVel)
            {
                vel += velocity/20;
            }
            velocity = vel - vecY / 100;
            if (velocity <= vel/10)
            {
                velocity = vel / 10;
            }
            transform.position += new Vector3(0, velocity, 0);
        }
        else
        {
            if (vel < initVel)
            {
                vel += velocity/20;
            }
            velocity = vel - (maxheight - vecY) / 100;
            if (velocity <= vel/10)
            {
                velocity = vel/10;
            }
            transform.position -= new Vector3(0, velocity, 0);
        }
    }
}
