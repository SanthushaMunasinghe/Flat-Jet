using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamOthoSize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer boundary;

    [SerializeField] private CinemachineVirtualCamera cmvc;

    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = boundary.bounds.size.x / boundary.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            cmvc.m_Lens.OrthographicSize = boundary.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            cmvc.m_Lens.OrthographicSize = boundary.bounds.size.y / 2 * differenceInSize;
        }
    }
}
