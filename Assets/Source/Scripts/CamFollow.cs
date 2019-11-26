using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    protected Transform player;
    private Vector3 distance;
    private float minXPos;
    private float maxXPos;
    void Start()
    {
        player = Player.Instance.transform;
        distance = transform.position;
        minXPos = GameObject.Find("LeftWall").transform.position.x + 4.5f;
        maxXPos = GameObject.Find("RightWall").transform.position.x - 4.5f;
    }

    // transform.position = new Vector3(0, 0, player.position.z) + distance;
    void Update()
    {
        if(minXPos <= player.position.x && player.position.x <= maxXPos)
        {
            transform.position = player.position + distance;
        }
        else if( minXPos > player.position.x)
        {
            transform.position = new Vector3(minXPos, player.position.y, player.position.z) + distance;
        }
        else if(maxXPos < player.position.x)
        {
            transform.position = new Vector3(maxXPos, player.position.y, player.position.z) + distance;
        }
    }
}
