using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using static BulletManager;

public static class BulletPulling
{
    public static Transform player;
    public static Queue<Transform> bulletQueue = new Queue<Transform>();
    //public static Queue<BulletInfo> bulletQueue = new Queue<BulletInfo>();
    public static void Enqueue(Transform bullet)
    {
        bulletQueue.Enqueue(bullet);
        //bulletQueue[type].Enqueue(bulletInfo);
        bullet.gameObject.SetActive(false);

    }

    public static Transform Dequeue(Transform master)
    {
        Transform bullet;
        
        if (bulletQueue.Count > 0)
        {
            bullet = bulletQueue.Dequeue();
        }
        else
        {
            bullet = UnityEngine.Object.Instantiate(Resources.Load<Transform>(@"Prefebs\bullet"));
            Debug.Log("Bullet Created! by" + master.name);
            bullet.gameObject.SetActive(false);
        }
        switch(master.tag)
        {
            case "Player":
                bullet.tag = "PlayerBullet";
                break;
            case "Enemy":
                bullet.tag = "EnemyBullet";
                break;
        }
        bullet.position = master.position;
        bullet.rotation = master.rotation;
        bullet.gameObject.SetActive(true);
        return bullet;
        
    }
}

