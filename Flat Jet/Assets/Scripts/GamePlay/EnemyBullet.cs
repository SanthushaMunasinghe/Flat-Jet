using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50.0f;

    [SerializeField] private float bulletLifetime = 1.0f;
    [SerializeField] private float bulletLifetimeStart;

    private void OnEnable()
    {
        bulletLifetimeStart = bulletLifetime;
    }

    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;

        bulletLifetimeStart -= Time.deltaTime;

        if (bulletLifetimeStart <= 0)
        {
            BasePool.Instance.enemyBulletPool.Release(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            BasePool.Instance.enemyBulletPool.Release(gameObject);
        }
    }
}
