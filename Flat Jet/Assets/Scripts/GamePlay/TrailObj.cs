using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailObj : MonoBehaviour
{
    //public TrailEffect trailEffect;

    [SerializeField] private float lifeTime;
    public float elapsedTime = 0.0f;

    public bool isComplete = false;

    public float colorAlpha;

    public Color myColor;
    public Vector2 startSize;
    public Vector2 endSize;

    void OnEnable()
    {
        elapsedTime = 0.0f;
        isComplete = false;
    }

    void Update()
    {
        if (!isComplete)
        {
            elapsedTime += Time.deltaTime;
            float completion = elapsedTime / lifeTime;

            //transform.localScale = Vector3.Lerp(trailEffect.startSize, trailEffect.endSize, completion);
            transform.localScale = Vector3.Lerp(startSize, endSize, completion);

            colorAlpha = Mathf.Lerp(1, 0, completion);
            GetComponent<SpriteRenderer>().color = new Color(myColor.r, myColor.g, myColor.b, colorAlpha);

            if (transform.localScale.x >= endSize.x)
            {
                isComplete = true;
                BasePool.Instance.trailPool.Release(gameObject);
            }
        }
    }
}
