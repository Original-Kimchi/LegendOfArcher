using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float originHp;
    [SerializeField]
    protected float hp;
    [SerializeField]
    public static float atkPower;
    [SerializeField]
    protected float atkSpeed;
    [SerializeField]
    protected Transform player;
    [SerializeField]
    protected float speed;
    protected bool isAttacking;
    protected float originSpeed;

    protected virtual IEnumerator Attacking()
    {
        yield return 0;
    }
    /*private void OnDisable()
    {
        EnemyManager.RemoveEnemy(gameObject);
    }*/
    protected void DefaultSetting()
    {
        isAttacking = false;
        originSpeed = speed;
        originHp = hp;
        atkPower = 1f;
        player = Player.Instance.transform;
        EnemyManager.AddEnemy(gameObject);
    }
    
    protected void Loop()
    {
        if (hp <= 0)
        {
            EnemyManager.RemoveEnemy(gameObject);
            Player.Instance.targeting = false;
            gameObject.SetActive(false);
        }
    }

    protected void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.CompareTag("PlayerBullet"))
        {
            Attacked(Player.Instance.atkPower,obj.gameObject);
        }
    }

    protected void Attacked(float power,GameObject obj)
    {
        BulletPulling.Enqueue(obj.transform);
        hp -= power;
    }
}
