using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // hp bar
    [SerializeField]
    private Image HPBarPref;
    [SerializeField]
    private Image HPBar;
    [SerializeField]
    private Transform HPBarUI;
    private Image HPImage;
    private Text HPText;

    protected bool IsAttacking { get; set; }
    protected float OriginSpeed { get; set; }
    protected Rigidbody rigibody { get; set; }
    

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
        IsAttacking = false;
        OriginSpeed = speed;
        originHp = hp;
        atkPower = 1f;
        player = Player.Instance.transform;
        EnemyList.AddEnemy(gameObject);
        rigibody = GetComponent<Rigidbody>();
        HPBar = Instantiate(HPBarPref, HPBarUI);
        HPImage = HPBar.transform.Find("EnemyHpBarHp").GetComponent<Image>(); // nullreper
        HPText = HPBar.transform.Find("Text").GetComponent<Text>();
        if (HPImage == null) Debug.Log(transform.name + "'s HPImage is null!");
        if (HPText == null) Debug.Log(transform.name + "'s HPText is null");

        StartCoroutine(Loop());
    }
    
    protected IEnumerator Loop()
    {
        
        WaitForFixedUpdate waiting = new WaitForFixedUpdate();
        while (true)
        {
            HPBar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 3f;
            HPImage.fillAmount = hp / originHp;
            HPText.text = hp.ToString();
            if (hp <= 0)
            {
                EnemyList.RemoveEnemy(gameObject);
                Player.Instance.targeting = false;
                gameObject.SetActive(false);
                HPBar.gameObject.SetActive(false);
            }
            yield return waiting;
        }
    }

    protected void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.CompareTag("PlayerBullet"))
        {
            Attacked(Player.Instance.atkPower,obj.gameObject);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                rigibody.isKinematic = true;
                break;
            default: break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rigibody.isKinematic = false;
    }

    protected void Attacked(float power,GameObject obj)
    {
        BulletPulling.Enqueue(obj.transform);
        hp -= power;
    }
}
