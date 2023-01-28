using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GridManager gridManager;

    [SerializeField] private float rotSpeed;
    [SerializeField] private float launchDistance;
    [SerializeField] private float aimDitance;

    [SerializeField] private float DamageArea;
    [SerializeField] private float rocketSpeed;

    private float distance;
    private bool isLaunch = false;

    public float RocketHealth = 5;
    private Color myColor;
    private Vector2 initialPos;
    [SerializeField] private Color redColor;

    public int rocketScore;
    private AudioSource explosionSFX;

    void Start()
    {
        gridManager = GridManager.Instance;

        initialPos = transform.position;

        GetComponent<SpriteRenderer>().color = gridManager.obsColors[Random.Range(0, gridManager.obsColors.Length)];
        myColor = GetComponent<SpriteRenderer>().color;

        explosionSFX = GameObject.Find("ExplosionSFX").GetComponent<AudioSource>();

        gameObject.transform.GetChild(0).GetChild(0).GetComponent<TrailEffect>().startColor = myColor;

        rocketScore = Random.Range(10, 21);

        StartCoroutine(RocketLifeTime());
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, gridManager.playerObj.transform.position);

        Vector2 currentRot = (gridManager.playerObj.transform.position - transform.position).normalized;

        if (distance <= aimDitance)
        {
            float rotAngle = Mathf.Atan2(-currentRot.x, currentRot.y) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.AngleAxis(rotAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);

            if (distance <= launchDistance)
            {
                isLaunch = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isLaunch)
        {
            Launching();
        }
    }

    private void Launching()
    {
        Vector2 dir = (gridManager.playerObj.transform.position - transform.position).normalized;

        GetComponent<Rigidbody2D>().AddForce(rocketSpeed * dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color colColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

        if (collision.gameObject.tag == "Player" && colColor != myColor && RocketHealth > 0)
        {
            RocketHealth -= 1;

            if (RocketHealth <= 0)
            {
                GameObject txtClone = BasePool.Instance.ScoreTxtPool.Get();
                txtClone.transform.position = transform.position;

                if (myColor == redColor)
                {
                    UIManager.Instance.score -= rocketScore;
                    txtClone.GetComponent<TextMeshPro>().color = redColor;
                    txtClone.GetComponent<TextMeshPro>().text = $"-{rocketScore}";
                }
                else
                {
                    UIManager.Instance.score += rocketScore;
                    txtClone.GetComponent<TextMeshPro>().color = Color.white;
                    txtClone.GetComponent<TextMeshPro>().text = $"+{rocketScore}";
                }
                explosionSFX.Play();
                DestroyRocket(colColor);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Color colColor = gridManager.playerObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        explosionSFX.Play();
        DestroyRocket(colColor);
    }

    private void DestroyRocket(Color color)
    {
        gridManager.spawnPoints.Add(initialPos);

        PlayerHealth playerHealth = gridManager.playerObj.transform.GetChild(0).GetComponent<PlayerHealth>();

        if (color != myColor && UIManager.Instance.playerHealth > 0 && distance <= DamageArea)
        {
            playerHealth.StartDamageAnimation();

            if (color == redColor)
            {
                playerHealth.DecreaseHealth(15);
            }
            else
            {
                playerHealth.DecreaseHealth(10);
            }
        }

        for (int i = 0; i < BasePool.Instance.destroyEffectsPool.Count; i++)
        {
            GameObject destClone = BasePool.Instance.destroyEffectsPool[i].Get();
            destClone.transform.position = transform.position;
            var main = destClone.GetComponent<ParticleSystem>().main;
            main.startColor = myColor;
        }

        transform.parent.GetComponent<ActivateChild>().DestroyUs(2);
    }

    private IEnumerator RocketLifeTime()
    {
        yield return new WaitForSeconds(Random.Range(60.0f, 91.0f));
        if (distance > 20)
        {
            DestroyRocket(myColor);
        }
    }
}
