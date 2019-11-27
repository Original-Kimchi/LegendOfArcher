using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    private void OncCollisionEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(transform.name + "is Attaked!");

            Player.Instance.Damage();
        }
    }
}
