using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetMain : MonoBehaviour
{
    [SerializeField] private GameObject[] jetPrefabs;

    public Color[] jetColors;

    private GameObject playerJetPrefab;
    private TrailEffect playerTrail;
    private SpriteRenderer playerSprite;

    void Start()
    {
        spawnJet();
    }
    void Update()
    {
        changeColor();
    }

    private void spawnJet()
    {
        int randI = Random.Range(0, jetPrefabs.Length);

        playerJetPrefab = Instantiate(jetPrefabs[randI], transform.position, Quaternion.identity);
        playerJetPrefab.transform.parent = transform;
        playerSprite = playerJetPrefab.GetComponent<SpriteRenderer>();
        playerTrail = playerJetPrefab.GetComponentInChildren<TrailEffect>();

        playerJetPrefab.GetComponent<PlayerHealth>().myIndex = randI;
    }

    private void changeColor()
    {
        Color startColor = jetColors[PlayerInputManager.Instance.colorIndex];
        playerSprite.color = startColor;

        playerTrail.startColor = startColor;
    }
}
