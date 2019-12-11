using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon
{
    public override void Attack(GameObject target)
    {
        PlayerComponent.transform.LookAt(target.transform.position);
        BulletPulling.Dequeue(PlayerComponent.transform);
    }
}
