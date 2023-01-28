using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] private GameObject trailSprite;

    public Color startColor;

    public Vector2 startSize;
    public Vector2 endSize;

    [SerializeField] private float spawnRate;
    private float startSpawn;

    public Rigidbody2D rb;
    public Vector2 velocity;

    void Start()
    {
        rb = transform.parent.transform.parent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        velocity = rb.velocity;

        if (startSpawn <= 0 && (rb.velocity.x > 0.5f || rb.velocity.x < -0.5f || rb.velocity.y > 0.5f || rb.velocity.y < -0.5f))
        {
            //GameObject trailObjClone = Instantiate(trailSprite, transform.position, transform.rotation);
            GameObject trailObjClone = BasePool.Instance.trailPool.Get();
            trailObjClone.transform.position = transform.position;
            trailObjClone.transform.rotation = transform.rotation;
            trailObjClone.transform.localScale = startSize;

            trailObjClone.GetComponent<TrailObj>().myColor = startColor;
            trailObjClone.GetComponent<SpriteRenderer>().color = startColor;

            trailObjClone.GetComponent<TrailObj>().startSize = startSize;
            trailObjClone.GetComponent<TrailObj>().endSize = endSize;
            startSpawn = spawnRate;
        }
        else
        {
            startSpawn -= Time.deltaTime;
        }
    }
}
