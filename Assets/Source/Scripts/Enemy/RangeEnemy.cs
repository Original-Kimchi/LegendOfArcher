using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{

    private void Start()
    {
        DefaultSetting();
        StartCoroutine(Move());
        StartCoroutine(Attacking());
    }
    private void FixedUpdate()
    {
        // transform.Translate(transform.forward * Time.deltaTime * speed);
        rigibody.AddForce(transform.forward * Time.deltaTime * speed * 25f);
    }

    private IEnumerator Move()
    {
        Quaternion lookRotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
        WaitForSeconds waiting = new WaitForSeconds(2f);
        while(true)
        {
            if (!IsAttacking)
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
            IsAttacking = true;
            yield return reaction;  // 반동(경직)
            transform.GetComponent<Renderer>().material.color = Color.green;
            IsAttacking = false;
            speed = OriginSpeed;
            yield return waiting;
        }
    }

    protected void Attack()
    {
        BulletPulling.Dequeue(transform);
    }
}