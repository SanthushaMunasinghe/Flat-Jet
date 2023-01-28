using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetController : MonoBehaviour
{
    private Vector2 getMoveDir;

    public Rigidbody2D playerRb;

    public float initialMoveSpeed = 4.0f;
    public float moveSpeed;
    [SerializeField] private float rotSpeed = 3.0f;

    public Vector2 currentRot;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerJetMove();

        if (UIManager.Instance.isGameOver)
        {
            moveSpeed = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        playerRb.AddForce(transform.up * moveSpeed);

        playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, moveSpeed);
    }

    private void PlayerJetMove()
    {
        getMoveDir = PlayerInputManager.Instance.GetMovementDir().normalized;

        if (PlayerInputManager.Instance.isRot && !UIManager.Instance.isGameOver)
        {
            currentRot = getMoveDir;
        }

        float rotAngle = Mathf.Atan2(-currentRot.x, currentRot.y) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(rotAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
    }
}
