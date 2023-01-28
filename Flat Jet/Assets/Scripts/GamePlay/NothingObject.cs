using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingObject : MonoBehaviour
{
    public GridManager gridManager;

    void Start()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(Random.Range(60.0f, 91.0f));

        gridManager.spawnPoints.Add(transform.position);
        Destroy(gameObject);
    }
}
