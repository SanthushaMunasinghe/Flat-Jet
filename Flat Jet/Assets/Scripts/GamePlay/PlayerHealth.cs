using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Color myColor;
    [SerializeField] private Color redColor;

    private float animInterval = 1.0f;
    private float startAnim;

    private Animation colAnim;
    public int myIndex;
    [SerializeField] private string[] animNames;

    private AudioSource playerHurtSFX;

    void Start()
    {
        colAnim = GetComponent<Animation>();
        playerHurtSFX = GameObject.Find("PlayerHurt").GetComponent<AudioSource>();
    }

    void Update()
    {
        myColor = GetComponent<SpriteRenderer>().color;

        if (startAnim <= 0)
        {
            colAnim.Stop($"{animNames[myIndex]}");
        }
        else
        {
            startAnim -= Time.deltaTime;
        }
    }

    public void DecreaseHealth(int value)
    {
        UIManager.Instance.playerHealth -= value;
        playerHurtSFX.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Color colColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

        if (colColor != myColor && UIManager.Instance.playerHealth > 0)
        {
            if (colColor == redColor)
            {
                DecreaseHealth(8);
            }
            else
            {
                DecreaseHealth(4);
            }
        }

        if (collision.gameObject.tag == "Boundary")
        {
            UIManager.Instance.playerHealth = 0;
        }

        StartDamageAnimation();
    }

    public void StartDamageAnimation()
    {
        colAnim.Play($"{animNames[myIndex]}");
        startAnim = animInterval;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color colColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

        if (collision.gameObject.tag == "Enemy" && colColor != myColor && UIManager.Instance.playerHealth > 0)
        {
            if (colColor == redColor)
            {
                DecreaseHealth(2);
            }
            else
            {
                DecreaseHealth(1);
            }
        }
    }
}
