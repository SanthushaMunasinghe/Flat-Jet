using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private JetController jetController;

    [SerializeField] private GameObject loadingPanel;

    [SerializeField] private Image loadingBar;

    private float loadingValue = 0.0f;

    void Start()
    {
        jetController.moveSpeed = 0.0f;
    }

    void Update()
    {
        if (loadingValue <= 5)
        {
            loadingValue += Time.deltaTime;
            loadingPanel.SetActive(true);
        }
        else
        {
            jetController.moveSpeed = jetController.initialMoveSpeed;
            loadingPanel.SetActive(false);
        }

        loadingBar.fillAmount = (loadingValue * 2)/10;
    }
}
