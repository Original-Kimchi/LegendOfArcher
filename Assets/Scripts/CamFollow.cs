using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    protected Transform player;
    private Vector3 distance;
    void Start()
    {
        distance = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(0, 0, player.position.z) + distance;
    }
}
