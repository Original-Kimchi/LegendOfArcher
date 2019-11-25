using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float liveTime;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Renderer objectRenderer;
    public float damage;

    private void OnEnable()
    {
        BulletSetting();
    }

    private void BulletSetting()
    {
        liveTime = 15f;
        switch (transform.tag)
        {
            case "PlayerBullet":
                speed = 5f;
                objectRenderer.material.color = Color.green;
                break;
            case "EnemyBullet":
                speed = 2f;
                objectRenderer.material.color = Color.red;
                break;
            default:
                speed = 0f;
                objectRenderer.material.color = Color.black;
                break;
        }
    }

    private void FixedUpdate()
    {
        // Debug.Log("BulletScript Updating");
        if(liveTime > 0)
        {
            // Debug.Log("BulletScript if pass");
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            liveTime -= Time.deltaTime;
        }
        else
        {
            BulletPulling.Enqueue(transform);
        }
    }

    /*private static List<BulletInfo> liveBullets = new List<BulletInfo>();
    public static Transform player;
    public static float liveTime;
    public static float speed;
    private static Coroutine coroutine;

    public static IEnumerator MoveBullet()
    {
        var direction = player.forward;
        WaitForEndOfFrame dongTak = new WaitForEndOfFrame();
        while (true)
        {
            while (liveBullets.Count > 0)
            {
                Debug.Log("Ally's BulletsList size:" + liveBullets.Count);
                for (int i = 0; i < liveBullets.Count; i++)
                {
                    if (liveBullets[i].liveTime > 0 && liveBullets[i].bullet.gameObject.activeSelf)
                    {
                        Debug.DrawRay(liveBullets[i].bullet.position, liveBullets[i].bullet.forward * 10f);
                        liveBullets[i].bullet.Translate(direction * Time.deltaTime * speed);
                        liveBullets[i].liveTime -= Time.deltaTime;
                    }
                    else
                    {
                        BulletPulling.Enqueue(bulletType.Player,liveBullets[i]);
                        liveBullets.RemoveAt(i);
                        i--;
                    }
                }

                yield return dongTak;
            }
            Debug.Log("out while! liveBullet size: " + liveBullets.Count);
            yield return dongTak;
        }
    }

    public static void CreateBullet()
    {
        BulletInfo bulletInfo = BulletPulling.Dequeue(bulletType.Player);

        if (bulletInfo == null)
        {
            bulletInfo = new BulletInfo();
            bulletInfo.bullet = MonoBehaviour.Instantiate(Resources.Load<Transform>(@"Prefebs\bullet"));
            Debug.Log("bullet created");
        }
        bulletInfo.bullet.position = player.position;
        bulletInfo.bullet.rotation = player.rotation;
        bulletInfo.liveTime = liveTime;
        liveBullets.Add(bulletInfo);
    }

    public static void StartCoroutine()
    {
        coroutine = BulletCoroutineManager.instance.StartCoroutine(MoveBullet());
    }

    public static void StopCoroutine()
    {
        BulletCoroutineManager.instance.StopCoroutine(coroutine);
    }*/
}