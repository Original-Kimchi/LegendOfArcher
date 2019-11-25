﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            BulletPulling.Enqueue(other.transform);
        }
    }
}