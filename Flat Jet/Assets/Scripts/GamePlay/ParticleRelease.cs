using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRelease : MonoBehaviour
{
    [SerializeField] private int index;

    private void OnEnable()
    {
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(2.0f);
        BasePool.Instance.destroyEffectsPool[index].Release(gameObject);
    }
}
