using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Player PlayerComponent { get; private set; }
    protected float atkPower { get; }
    protected float atkSpeed { get; }

    public void Init(Player player)
    {
        PlayerComponent = player;
    }

    public abstract void Attack(GameObject target);
}
