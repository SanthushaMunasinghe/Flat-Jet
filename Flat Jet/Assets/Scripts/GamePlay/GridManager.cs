using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;

    [SerializeField] private Transform GridParent;
    [SerializeField] private Vector2 gridOrigin;
    [SerializeField] private float gridSize;
    [SerializeField] private float cellSize;

    public List<Vector2> spawnPoints = new List<Vector2>();

    [SerializeField] private Transform obsParent;
    [SerializeField] private Transform gunParent;
    [SerializeField] private Transform rocketParent;
    [SerializeField] private Transform diamondParent;
    [SerializeField] private Transform healthParent;
    [SerializeField] private Transform nothingParent;
    [SerializeField] private GameObject obstacle;
    public Color[] obsColors;
    public GameObject sprite;
    public GameObject diamond;
    public List<GameObject> otherSpawns;
    public GameObject NothingObj;

    public GameObject playerObj;

    public static GridManager Instance
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

    void Start()
    {
        GenerateGrid();
        GenetateObstacles();
    }

    void Update()
    {
        if (spawnPoints.Count != 0)
        {
            StartCoroutine(GenerateOtherSpawns());
        }
    }

    private void GenerateGrid()
    {
        for (float x = 0; x < gridSize; x += cellSize)
        {
            for (float y = 0; y < gridSize; y += cellSize)
            {
                Vector2 spawnPos = new Vector2(x + gridOrigin.x, y + gridOrigin.y);

                spawnPoints.Add(spawnPos);
            }
        }
    }

    private void GenetateObstacles()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (i % 2 == 1)
            {
                GameObject obstacleClone = Instantiate(obstacle, spawnPoints[i], Quaternion.identity);
                obstacleClone.name = $"Obstacle {i}";
                obstacleClone.transform.parent = obsParent.transform;
                obstacleClone.GetComponent<Obstacle>().gridManager = this.GetComponent<GridManager>();
                spawnPoints.RemoveAt(i);
            }
        }
    }

    private IEnumerator GenerateOtherSpawns()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int randValue = Random.Range(0, 2);

            if (spawnPoints[i] != Vector2.zero && randValue == 0)
            {
                int percentage = Random.Range(0, 100);
                if (percentage < 40)
                {
                    //GameObject otherSpawnClone = Instantiate(otherSpawns[0], spawnPoints[i], Quaternion.identity);
                    GameObject otherSpawnClone = BasePool.Instance.starSpawnerPool.Get();
                    otherSpawnClone.transform.position = spawnPoints[i];
                    otherSpawnClone.transform.parent = diamondParent.transform;
                }
                else if(percentage < 75)
                {
                    //GameObject otherSpawnClone = Instantiate(otherSpawns[1], spawnPoints[i], Quaternion.identity);
                    GameObject otherSpawnClone = BasePool.Instance.gunPool.Get();
                    otherSpawnClone.transform.position = spawnPoints[i];
                    otherSpawnClone.transform.parent = gunParent.transform;
                    BasePool.Instance.gunCount++;
                }
                else if (percentage < 85)
                {
                    GameObject otherSpawnClone = Instantiate(otherSpawns[2], spawnPoints[i], Quaternion.identity);
                    otherSpawnClone.transform.position = spawnPoints[i];
                    otherSpawnClone.transform.parent = rocketParent.transform;
                }
                else
                {
                    GameObject otherSpawnClone = Instantiate(otherSpawns[3], spawnPoints[i], Quaternion.identity);
                    otherSpawnClone.transform.position = spawnPoints[i];
                    otherSpawnClone.transform.parent = healthParent.transform;
                }
            }
            else
            {
                GameObject nothingObjSpawnClone = Instantiate(NothingObj, spawnPoints[i], Quaternion.identity);
                nothingObjSpawnClone.GetComponent<NothingObject>().gridManager = this.GetComponent<GridManager>();
                nothingObjSpawnClone.transform.parent = nothingParent.transform;
            }

            spawnPoints.RemoveAt(i);
        }
    }
}
