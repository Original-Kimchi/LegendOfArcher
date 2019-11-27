using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle, Move, Attack, Death
}

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }
    public int level;
    
    public float originHp;
    public float hp;
    [SerializeField] private float atkSpeed;
    [SerializeField] public Animator Anim;
    [SerializeField] private float damageCool;
    
    // hp bar
    [SerializeField] private Image HPBarPref;
    private Image HPbar;
    [SerializeField] private Transform HpBarUI;
    private Image HPImage;
    private Text HPText;

    private Rigidbody rigibody;
    private Color originalColor;
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
        Instance = this;
        playerState = PlayerState.Idle;
        coroutine = Attacking();
        direction = new Vector3();
        rigibody = GetComponent<Rigidbody>();
        damageCool = 0;
    }

    private void Start()
    {
        HPbar = Instantiate(HPBarPref,HpBarUI);
        HPImage = HPbar.transform.Find("PlayerHPBarHP").GetComponent<Image>();
        HPText = HPbar.transform.Find("Text").GetComponent<Text>();
        originHp = hp;
    }
    private void FixedUpdate()
    {
        HPbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
        HPImage.fillAmount = hp / originHp;
        HPText.text = hp.ToString();
        if (damageCool > 0)
            damageCool -= Time.deltaTime;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            HPbar.gameObject.SetActive(false);
        }

        if(isWalking) 
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            rigibody.AddForce(transform.forward * moveSpeed * 25f * Time.deltaTime);
        }


    }

    public IEnumerator Attacking()
    {
        var enemies = GameManager.EnemyList;
        Transform targetEnemy = enemies[0].transform;
        WaitForSeconds waiting = new WaitForSeconds(1f);
        while (!isWalking)
        {
            Debug.Log("EnemyList Count: " + GameManager.EnemyList.Count);
            yield return waiting;
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
            
        }
    }

    public void Damage()
    {
        if(damageCool <= 0)
        {
            hp -= Enemy.atkPower;
            damageCool = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "EnemyBullet":
                BulletPulling.Enqueue(other.transform);
                Damage();
                break;
            case "Enemy":
                Damage();
                break;
            default:break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                Damage();
                break;
            default: break;
        }
    }
}
