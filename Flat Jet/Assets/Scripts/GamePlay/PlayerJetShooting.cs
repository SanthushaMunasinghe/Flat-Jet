using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetShooting : MonoBehaviour
{
    private PlayerJetMain playerJetMain;

    [SerializeField] private GameObject playerbullet;
    [SerializeField] private Transform[] shotPoints;

    [SerializeField] private float shotRate = 0.5f;
    private float startShot = 0;

    private AudioSource playerShootSFX;

    void Start()
    {
        playerJetMain = GameObject.Find("Player").GetComponent<PlayerJetMain>();
        playerShootSFX = GameObject.Find("PlayerShootSFX").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PlayerInputManager.Instance.isShoot)
        {
            if (UIManager.Instance.shootLitmit < 5 && !UIManager.Instance.isGameOver)
            {
                Shoot();
                UIManager.Instance.shootLitmit += Time.deltaTime;
            }
            else
            {
                UIManager.Instance.cantShootTxt.SetActive(true);
                UIManager.Instance.shootLimitImgBg.GetComponent<Animation>().Play("ShootLimit");
            }
        }
        else if (UIManager.Instance.shootLitmit > 1)
        {
            UIManager.Instance.shootLitmit -= Time.deltaTime;
            UIManager.Instance.cantShootTxt.SetActive(false);
            UIManager.Instance.shootLimitImgBg.GetComponent<Animation>().Stop("ShootLimit");
            UIManager.Instance.shootLimitImgBg.color = Color.white;
            UIManager.Instance.shootLimitImg.color = Color.white;
        }
    }

    private void Shoot()
    {
        if (startShot <= 0)
        {
            for (int i = 0; i < shotPoints.Length; i++)
            {
                //GameObject bulletClone = Instantiate(playerbullet, shotPoints[i].position, transform.rotation);
                BasePool.Instance.bulletCount++;
                GameObject bulletClone = BasePool.Instance.playerBulletPool.Get();
                bulletClone.transform.position = shotPoints[i].position;
                bulletClone.transform.rotation = transform.rotation;
                bulletClone.GetComponent<SpriteRenderer>().color = playerJetMain.jetColors[PlayerInputManager.Instance.colorIndex];
                playerShootSFX.Play();
            }

            startShot = shotRate;
        }
        else
        {
            startShot -= Time.deltaTime;
        }
    }
}
