using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GridManager gridManager;

    public List<Vector2> positions = new List<Vector2>();

    private float[] rotValues = { 0.0f, 45.0f, 90.0f, 135.0f, 180.0f, 225.0f, 270.0f, 315.0f };

    void Start()
    {
        CretaePos();
        GenerateSprites();
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

    private void GenerateSprites()
    {
        int randCount = Random.Range(1, 4);
        int count = 0;

        while (count < randCount)
        {
            int randindex = Random.Range(0, positions.Count);
            Vector2 randPos = positions[randindex];

            SpawnSprite(randPos);
            positions.RemoveAt(randindex);
            count++;
        }
    }

    private void SpawnSprite(Vector2 pos)
    {
        int randRot = Random.Range(0, rotValues.Length);
        int randScale = Random.Range(3, 7);

        GameObject spriteClone = Instantiate(gridManager.sprite, pos, Quaternion.Euler(0.0f, 0.0f, rotValues[randRot]));
        spriteClone.transform.parent = gameObject.transform;
        spriteClone.name = $"sprite {pos}";
        spriteClone.transform.localScale = new Vector3(0.2f, randScale, 0);
        spriteClone.GetComponent<SpriteRenderer>().color = gridManager.obsColors[Random.Range(0, gridManager.obsColors.Length)];
    }
}
