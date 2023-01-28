using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public GridManager gridManager;

    [SerializeField] private float rotSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform[] shotPoints;

    [SerializeField] private List<float> shotRates;
    private float startShot = 0;

    private AudioSource gunShootSFX;
    private AudioSource explosionSFX;

    private float distance;

    public float gunHealth = 10;
    private Color myColor;
    [SerializeField] private Color redColor;

    public int gunScore;

    [SerializeField] private float DamageArea;

    private void OnEnable()
    {
        gridManager = GridManager.Instance;

        GetComponent<SpriteRenderer>().color = gridManager.obsColors[Random.Range(0, gridManager.obsColors.Length)];
        myColor = GetComponent<SpriteRenderer>().color;

        gunShootSFX = GameObject.Find("GunShootSFX").GetComponent<AudioSource>();
        explosionSFX = GameObject.Find("ExplosionSFX").GetComponent<AudioSource>();

        gunHealth = Random.Range(10, 21);

        StartCoroutine(GunLifeTime());

        gunScore = Random.Range(5, 11);

        GetComponent<Animation>().Play("GunSpawn");
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, gridManager.playerObj.transform.position);

        Vector2 currentRot = (gridManager.playerObj.transform.position - transform.position).normalized;

        if (distance <= minDistance)
        {
            float rotAngle = Mathf.Atan2(-currentRot.x, currentRot.y) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.AngleAxis(rotAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);

            Shooting();
        }
    }

    private void Shooting()
    {
        if (startShot <= 0)
        {
            for (int i = 0; i < shotPoints.Length; i++)
            {
                GameObject bulletClone = BasePool.Instance.enemyBulletPool.Get();
                bulletClone.transform.position = shotPoints[i].position;
                bulletClone.transform.rotation = transform.rotation;
                bulletClone.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
                gunShootSFX.Play();
            }

            int randRate = Random.Range(0, shotRates.Count);

            startShot = shotRates[randRate];
        }
        else
        {
            startShot -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color colColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

        if (collision.gameObject.tag == "Player" && colColor != myColor && gunHealth > 0)
        {
            gunHealth -= 1;

            if (gunHealth <= 0)
            {
                GameObject txtClone = BasePool.Instance.ScoreTxtPool.Get();
                txtClone.transform.position = transform.position;

                if (myColor == redColor)
                {
                    UIManager.Instance.score -= gunScore;
                    txtClone.GetComponent<TextMeshPro>().color = redColor;
                    txtClone.GetComponent<TextMeshPro>().text = $"-{gunScore}";
                }
                else
                {
                    UIManager.Instance.score += gunScore;
                    txtClone.GetComponent<TextMeshPro>().color = Color.white;
                    txtClone.GetComponent<TextMeshPro>().text = $"+{gunScore}";
                }

                explosionSFX.Play();
                DestroyGun(colColor);
            }
        }
    }

    private void DestroyGun(Color color)
    {
        gridManager.spawnPoints.Add(transform.position);

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

        BasePool.Instance.gunCount--;
        transform.parent.GetComponent<ActivateChild>().DestroyUs(1);
    }

    private IEnumerator GunLifeTime()
    {
        yield return new WaitForSeconds(Random.Range(60.0f, 91.0f));
        if (distance > maxDistance )
        {
            DestroyGun(myColor);
        }
    }
}
