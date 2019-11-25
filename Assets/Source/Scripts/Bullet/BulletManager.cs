using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager
{

    public enum bulletType : int{ NULL = -1, Player = 0, Enemy = 1 }
    public class BulletInfo
    {
        public Transform bullet;
        public float liveTime;
        public int bulletNum;
        public Transform gunner;
    }

    public class BulletInitalizeData
    {
        public float liveTime;
        public float speed;
    }


}