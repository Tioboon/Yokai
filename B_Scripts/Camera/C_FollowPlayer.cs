using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FollowPlayer : MonoBehaviour
{
    public float yPos = 0;
    public float lerpVariation;
    private GameObject player;
    private float amount;
    public bool inverted;
    
    void Start()
    {
        player = GameObject.Find("Character");
        InvokeRepeating("CheckCamDistance", 0, 0.1f);
    }
    
    void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, yPos, -10),
                    new Vector3(player.transform.position.x, 0, 0) + new Vector3(+0, yPos, -10), amount/10);
    }

    private void CheckCamDistance()
    {
        if(amount < 1)
        {
            amount += lerpVariation;
        }
        else
        {
            amount = 1;
        }
    }
}
