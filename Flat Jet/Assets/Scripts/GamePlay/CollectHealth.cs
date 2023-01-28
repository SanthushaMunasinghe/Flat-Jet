using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectHealth : MonoBehaviour
{
    public GridManager gridManager;

    private AudioSource collectHealthSFX;

    private void Start()
    {
        gridManager = GridManager.Instance;
        collectHealthSFX = GameObject.Find("CollectHealthSFX").GetComponent<AudioSource>();

        StartCoroutine(Lifetime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randValue = Random.Range(10, 26);

        if (collision.gameObject.tag == "Player" && UIManager.Instance.playerHealth > 0)
        {
            GameObject txtClone = BasePool.Instance.ScoreTxtPool.Get();
            txtClone.transform.position = transform.position;
            txtClone.GetComponent<TextMeshPro>().color = Color.white;
            txtClone.GetComponent<TextMeshPro>().text = $"{randValue}%";

            for (int i = 0; i < randValue; i++)
            {
                if (UIManager.Instance.playerHealth < 100)
                {
                    UIManager.Instance.playerHealth ++;
                }
            }
        }

        gridManager.spawnPoints.Add(transform.position);
        collectHealthSFX.Play();
        transform.parent.GetComponent<ActivateChild>().DestroyUs(3);
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(Random.Range(60.0f, 91.0f));

        gridManager.spawnPoints.Add(transform.position);
        transform.parent.GetComponent<ActivateChild>().DestroyUs(3);
    }
}
