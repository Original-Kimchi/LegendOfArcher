using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle, Move, Attack, Death
}

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public float originHp;
    public float hp;
    [SerializeField]
    protected float atkSpeed;
    [SerializeField]
    public Animator Anim;
    [SerializeField]
    protected Transform firePos;
    [SerializeField]
    protected Transform signObject;
    protected Rigidbody rigibody;
    protected Color originalColor;
    public float atkPower;
    // joyStick 관리
    public bool isWalking;
    public PlayerState playerState;
    public Vector3 direction;
    public float moveSpeed;
    public bool targeting;
    public IEnumerator coroutine { get; private set; }

    private void Awake()
    {
        originalColor = signObject.GetComponent<Renderer>().material.color;
        Instance = this;
        playerState = PlayerState.Idle;
        coroutine = Attacking();
        direction = new Vector3();
        rigibody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originHp = hp;
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 5, Color.white);
        // Debug.Log("isWalking: " + isWalking+", m oveSpeed:" + moveSpeed);
        if(isWalking)
            transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
        if(isWalking) 
        {
            rigibody.AddForce(transform.forward * moveSpeed * 25f * Time.deltaTime);
            // transform.Translate(Vector3.forward * moveSpeed * 0.5f * Time.deltaTime);
        }
    }

    public IEnumerator Attacking()
    {
        var enemies = EnemyManager.enemies;
        Transform targetEnemy = enemies[0].transform;
        while (!isWalking)
        {
            if (enemies.Count > 0)
            {
                if (!targeting)
                {
                    targetEnemy = enemies[0].transform;
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (Vector3.SqrMagnitude(transform.position - targetEnemy.transform.position) > Vector3.SqrMagnitude(transform.position - enemies[i].transform.position))
                        {
                            targetEnemy = enemies[i].transform;
                        }
                    }

                    targeting = true;
                }
                transform.LookAt(targetEnemy);
                BulletPulling.Dequeue(transform);
                Debug.Log(name + " is Attacking!");
            }
            
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "EnemyBullet":
                BulletPulling.Enqueue(other.transform);
                hp -= Enemy.atkPower;
                break;
            case "Enemy":
                hp -= Enemy.atkPower;
                break;
            default:break;
        }
        
    }
}
