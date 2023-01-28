using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTxtMove : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.0f;

    [SerializeField] private float speed = 2.0f;

    private void OnEnable()
    {
        lifeTime = 3.0f;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            BasePool.Instance.ScoreTxtPool.Release(gameObject);
        }
    }
}
