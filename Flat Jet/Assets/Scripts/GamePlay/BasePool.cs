using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BasePool : MonoBehaviour
{
    private static BasePool _instance;

    public ObjectPool<GameObject> trailPool;

    public ObjectPool<GameObject> gunPool;
    public ObjectPool<GameObject> starSpawnerPool;
    public ObjectPool<GameObject> starPool;

    public ObjectPool<GameObject> playerBulletPool;
    public ObjectPool<GameObject> enemyBulletPool;

    public List<ObjectPool<GameObject>> destroyEffectsPool = new List<ObjectPool<GameObject>>();

    public ObjectPool<GameObject> ScoreTxtPool;

    [SerializeField] private GameObject trailPrefab;

    [SerializeField] private GameObject starPrefab;

    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;

    [SerializeField] private GameObject[] destroyEffects;

    [SerializeField] private GameObject ScoreTxtPrefab;

    public int gunCount = 0;
    public int bulletCount = 0;

    public static BasePool Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        trailPool = NewPool(trailPrefab, 1000, 1000);
        starSpawnerPool = NewPool(GridManager.Instance.otherSpawns[0], 100, 241);
        starPool = NewPool(starPrefab, 250, 500);
        gunPool = NewPool(GridManager.Instance.otherSpawns[1], 100, 241);

        playerBulletPool = NewPool(playerBulletPrefab, 60, 100);
        enemyBulletPool = NewPool(enemyBulletPrefab, 100, 150);

        for (int i = 0; i < destroyEffects.Length; i++)
        {
            destroyEffectsPool.Add(NewPool(destroyEffects[i], 50, 150));
        }

        ScoreTxtPool = NewPool(ScoreTxtPrefab, 10, 15);
    }

    private ObjectPool<GameObject> NewPool(GameObject prefab, int minValue = 241, int maxValue = 300, bool check = false)
    {
        return new ObjectPool<GameObject>(() => 
            {
                return Instantiate(prefab);
            },
            (GameObject obj) => obj.gameObject.SetActive(true),
            (GameObject obj) => obj.gameObject.SetActive(false),
            (GameObject obj) => Destroy(obj),
            check,
            minValue,
            maxValue);
    }
}
