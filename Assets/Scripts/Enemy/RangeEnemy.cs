using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField]
    private Rigidbody rigibody;

    private void Start()
    {
        DefaultSetting();
        StartCoroutine(Move());
        StartCoroutine(Attacking());
    }

    private void FixedUpdate()
    {
        Loop();
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);
        transform.Translate(transform.forward * Time.deltaTime * speed);
        // rigibody.AddForce(transform.forward * Time.deltaTime * speed);
    }
    private IEnumerator Move()
    {
        Quaternion lookRotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
        WaitForSeconds waiting = new WaitForSeconds(1f);
        while(true)
        {
            if (!isAttacking)
            {
                lookRotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
                // speed = 0;
            }
            yield return waiting;
            // speed = originSpeed;
        }
    }
    protected override IEnumerator Attacking()
    {
        float random;
        float t;
        random = Random.Range(3f, 6f);
        WaitForSeconds reaction = new WaitForSeconds(2f);
        WaitForSeconds waiting = new WaitForSeconds(random - 1f);
        yield return new WaitForSeconds(5f);
        while(true)
        {
            speed = 0;
            t = Mathf.Atan2(player.position.z - transform.position.z, player.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 90 - t, 0);
            transform.GetComponent<Renderer>().material.color = Color.red;
            Attack();
            isAttacking = true;
            yield return reaction;  // 반동(경직)
            transform.GetComponent<Renderer>().material.color = Color.green;
            isAttacking = false;
            speed = originSpeed;
            yield return waiting;
        }
    }

    protected void Attack()
    {
        BulletPulling.Dequeue(transform);
    }
}