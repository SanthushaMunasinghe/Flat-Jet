using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GridManager gridManager;

    public List<Vector2> positions = new List<Vector2>();
    public List<GameObject> spawnedStars = new List<GameObject>();

    private bool isSpawned = false;
    public int spawnedCount = 0;

    private void OnEnable()
    {
        gridManager = GridManager.Instance;

        isSpawned = false;
        spawnedCount = 0;

        CretaePos();
        GenerateDiamonds();
        StartCoroutine(Lifetime());
    }

    void Update()
    {
        if (isSpawned && spawnedCount <= 0)
        {
            DesolveSelfe();
        }

        if (spawnedStars.Count != 0 && Vector2.Distance(transform.parent.position, gridManager.playerObj.transform.position) >= 15)
        {
            foreach (GameObject spawnedStar in spawnedStars)
            {
                spawnedStar.GetComponent<SpriteRenderer>().enabled = true;
                spawnedStar.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

    private void CretaePos()
    {
        for (float x = 0; x <= 5; x += 2.5f)
        {
            for (float y = 0; y <= 5; y += 2.5f)
            {
                Vector2 currentPos = (Vector2)transform.position + new Vector2(x + -2.5f, y + -2.5f);
                positions.Add(currentPos);
            }
        }
    }

    private void GenerateDiamonds()
    {
        spawnedCount = Random.Range(1, 5);
        int count = 0;

        while (count < spawnedCount)
        {
            int randindex = Random.Range(0, positions.Count);
            Vector2 randPos = positions[randindex];

            SpawnSprite(randPos);
            positions.RemoveAt(randindex);
            count++;
        }

        isSpawned = true;
    }

    private void SpawnSprite(Vector2 pos)
    {
        //GameObject spriteClone = Instantiate(gridManager.diamond, pos, Quaternion.Euler(0.0f, 0.0f, 45.0f));
        GameObject spriteClone = BasePool.Instance.starPool.Get();
        spriteClone.transform.position = pos;
        spriteClone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        spriteClone.transform.parent = gameObject.transform;
        spriteClone.name = $"sprite {pos}";
        spriteClone.GetComponent<SpriteRenderer>().color = gridManager.obsColors[Random.Range(0, gridManager.obsColors.Length)];
        spriteClone.GetComponent<Diamond>().diamondSpawner = this;
        spawnedStars.Add(spriteClone);
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(Random.Range(60.0f, 91.0f));

        DesolveSelfe();
    }

    private void DesolveSelfe()
    {
        gridManager.spawnPoints.Add(transform.position);

        foreach (GameObject spawnedStar in spawnedStars)
        {
            BasePool.Instance.starPool.Release(spawnedStar);
        }

        spawnedStars.Clear();
        positions.Clear();

        //Destroy(gameObject);
        BasePool.Instance.starSpawnerPool.Release(gameObject);
    }
}
