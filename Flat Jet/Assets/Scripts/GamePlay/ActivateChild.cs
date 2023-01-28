using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChild : MonoBehaviour
{
    [SerializeField] private GameObject childObj;

    private void OnEnable()
    {
        childObj.SetActive(false);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, GridManager.Instance.playerObj.transform.position) > 20)
        {
            childObj.SetActive(true);
        }
    }

    public void DestroyUs(int index)
    {
        if (index == 1)
        {
            BasePool.Instance.gunPool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
