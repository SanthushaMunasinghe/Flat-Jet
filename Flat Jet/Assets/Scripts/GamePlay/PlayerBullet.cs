using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50.0f;

    [SerializeField] private float initialBulletLifetime = 1.0f;
    [SerializeField] private float bulletLifetime = 1.0f;

    private void OnEnable()
    {
        bulletLifetime = initialBulletLifetime;
    }

    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;

        bulletLifetime -= Time.deltaTime;

        if (bulletLifetime <= 0)
        {
            BasePool.Instance.bulletCount--;
            BasePool.Instance.playerBulletPool.Release(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            BasePool.Instance.bulletCount--;
            BasePool.Instance.playerBulletPool.Release(gameObject);
        }
    }
}
