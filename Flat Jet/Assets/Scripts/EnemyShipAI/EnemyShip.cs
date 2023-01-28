using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    [SerializeField] private Transform player;
    public Vector2 playerPos;
    public Vector2 playerDir;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }
    void Update()
    {
        CalculatePlayerPos();

        if (playerDir != Vector2.zero)
        {
            EnemyRot();
        }
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    private void CalculatePlayerPos()
    {
        if (player != null) playerPos = player.position;
        else playerPos = transform.position;

        playerDir = (playerPos - (Vector2)transform.position).normalized;
    }

    private void EnemyMove()
    {
        enemyRb.AddForce(transform.up * moveSpeed);
    }

    private void EnemyRot()
    {
        float rotAngle = Mathf.Atan2(-playerDir.x, playerDir.y) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(rotAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
    }
}
