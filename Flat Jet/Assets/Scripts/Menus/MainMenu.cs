using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreTxt;

    [SerializeField] private TextMeshProUGUI warning;
    [SerializeField] private float warningTimeout = 0.0f;
    [SerializeField] private int confirmCount = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
        }

        highScoreTxt.text = $"HighScore : {PlayerPrefs.GetInt("highScore")}";
    }

    private void Update()
    {
        if (warningTimeout > 0)
        {
            if (confirmCount > 1)
            {
                PlayerPrefs.SetInt("highScore", 0);
                highScoreTxt.text = $"HighScore : {PlayerPrefs.GetInt("highScore")}";
                warning.text = "Confirmed!";
            }
            else
            {
                warning.text = "Press Again!";
            }

            warningTimeout -= Time.deltaTime;
        }
        else
        {
            confirmCount = 0;
            warning.text = "";
        }
    }

    public void CleareData()
    {
        warningTimeout = 5.0f;
        confirmCount++;
    }
}
