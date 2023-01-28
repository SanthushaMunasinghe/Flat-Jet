using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    private int highScore = 0;

    public int playerHealth = 10;
    [SerializeField] private Image healthBarValue;

    [SerializeField] private GameObject[] colors;

    public float shootLitmit = 1;
    public Image shootLimitImg;
    public Image shootLimitImgBg;
    public GameObject cantShootTxt;

    public bool isGameOver = false;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScore;

    public bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenu;

    public static UIManager Instance
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
        highScore = PlayerPrefs.GetInt("highScore");
    }

    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }

        scoreTxt.text = "Score : " + score.ToString();

        healthBarValue.fillAmount = (float)playerHealth / 100;
        shootLimitImg.fillAmount = (shootLitmit * 2) / 10;

        for (int i = 0; i < colors.Length; i++)
        {
            if (i == PlayerInputManager.Instance.colorIndex)
            {
                colors[i].SetActive(true);
            }
            else
            {
                colors[i].SetActive(false);
            }
        }

        GameOver();
    }

    private void GameOver()
    {
        if (playerHealth <= 0)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        if (highScore >= score)
        {
            finalScore.text = $"Your Score : {score}";
        }
        else if (highScore < score)
        {
            finalScore.text = $"Highscore : {score}";
            PlayerPrefs.SetInt("highScore", score);
        }

        yield return new WaitForSeconds(3.0f);

        GetComponent<SceneManagement>().LoadScene(0);
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void QuitRound()
    {
        Time.timeScale = 1;
        isGamePaused = false;

        GetComponent<SceneManagement>().LoadScene(0);
    }
}
