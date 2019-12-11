using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Weapon PlayerWeapon { get; private set; }
    
    public static Player Instance { get; private set; }
    public int level;
    
    public float maxHp;
    public float hp;
    [SerializeField] private float atkSpeed;
    [SerializeField] public Animator Anim;  //지우지마 지혁아
    [SerializeField] private float damageCool;
    
    // hp bar
    [SerializeField] private Slider HPBarPref;
    private Slider HPbar;
    [SerializeField] private Transform HpBarUI;
    //private Image HPImage;
    //private Text HPText;

    private Rigidbody rigibody;
    private Color originalColor;
    public float atkPower;
    private List<GameObject> enemies;
    private GameObject targetEnemy;
    // joyStick 관리
    // public bool isWalking;
    public Vector3 moveDir;
    public float moveSpeed;
    public bool targeting;
    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
        moveDir = new Vector3();
        rigibody = GetComponent<Rigidbody>();
        damageCool = 0;
    }

    private IEnumerator Start()
    {
        yield return null;
        enemies = GameManager.EnemyList;
        targetEnemy = enemies[0];               // 초기화
        HPbar = Instantiate(HPBarPref,HpBarUI);
        maxHp = hp;
    }
    private void FixedUpdate()
    {
        if (damageCool > 0)
            damageCool -= Time.deltaTime;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            HPbar.gameObject.SetActive(false);
        }
    }

    public void Movement()
    {
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        rigibody.AddForce(transform.forward * moveSpeed * 25f * Time.deltaTime);
    }
    // 공격 중 움직이면 공격 취소해야함
    public GameObject TargetSetting()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.SqrMagnitude(transform.position - targetEnemy.transform.position) > Vector3.SqrMagnitude(transform.position - enemies[i].transform.position))
                {
                    targetEnemy = enemies[i];
                }
            }
        }
        return targetEnemy;
    }
    public void StartAttack()
    {
        TargetSetting();
        coroutine = StartCoroutine(Attacking());
    }
    public void StopAttack()
    {
        StopCoroutine(coroutine);
    }

    public void Damage()
    {
        if (damageCool <= 0)
        {
            hp -= Enemy.atkPower;
            damageCool = 3;
        }
        else
        {
            Debug.Log("플레이어가 무적상태입니다.\n남은 무적시간:" + damageCool);
        }
    }

    public void LevelUp()
    {
        maxHp += 1;
        hp += 1;
        PlayerHPBar.instance.GetHpBoost();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "EnemyBullet":
                BulletPulling.Enqueue(other.transform);
                Damage();
                break;
            case "Enemy":
                Damage();
                break;
            case "Food":
                LevelUp();
                other.gameObject.SetActive(false);
                break;
            default: break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Damage();
                break;
            default: break;
        }
    }
    public IEnumerator Attacking()
    {
        WaitForSeconds waiting = new WaitForSeconds(1);
        float atkAngle;
        while (true)
        {

            Debug.Log("Player is targeting" + targetEnemy.name);
            if (enemies.Count > 0)
            {
                atkAngle = Mathf.Atan2(transform.position.z - targetEnemy.transform.position.z, transform.position.x - targetEnemy.transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, atkAngle, 0);
                // transform.LookAt(targetEnemy.transform);
                BulletPulling.Dequeue(transform);
            }
            yield return waiting;
        }
    }

    
}
