using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    public DiamondSpawner diamondSpawner;

    [SerializeField] private Color redColor;

    [SerializeField] private GameObject collectEffect;
    private AudioSource collectStarSFX;

    private int currentScore;

    private void Start()
    {
        collectStarSFX = GameObject.Find("CollectStarSFX").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color myColor = gameObject.GetComponent<SpriteRenderer>().color;
        Color colColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

        GameObject effectClone = Instantiate(collectEffect, transform.position, Quaternion.identity);
        var main = effectClone.GetComponent<ParticleSystem>().main;
        main.startColor = myColor;

        if (collision.gameObject.tag == "Player")
        {
            currentScore = Random.Range(1, 6);
            GameObject txtClone = BasePool.Instance.ScoreTxtPool.Get();
            txtClone.transform.position = transform.position;
            txtClone.GetComponent<TextMeshPro>().text = "";
            txtClone.GetComponent<TextMeshPro>().color = Color.white;


            if (myColor == redColor)
            {
                if (UIManager.Instance.score > 0)
                {
                    UIManager.Instance.score -= currentScore;
                    txtClone.GetComponent<TextMeshPro>().text = $"-{currentScore}";
                    txtClone.GetComponent<TextMeshPro>().color = myColor;
                }
            }
            else if (colColor == myColor)
            {
                UIManager.Instance.score += currentScore;
                txtClone.GetComponent<TextMeshPro>().text = $"+{currentScore}";
            }

            diamondSpawner.spawnedStars.Remove(gameObject);
            diamondSpawner.spawnedCount -= 1;
            collectStarSFX.Play();
        }

        BasePool.Instance.starPool.Release(gameObject);
    }
}
