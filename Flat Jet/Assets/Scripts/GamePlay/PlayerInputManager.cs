using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager _instance;

    private JetControls jetControls;

    public bool isShoot = false;
    public bool isRot = false;


    [SerializeField] private PlayerJetMain playerJetMain;
    public int colorIndex = 0;

    public static PlayerInputManager Instance
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

        jetControls = new JetControls();
    }

    private void OnEnable()
    {
        jetControls.Enable();
    }

    private void OnDisable()
    {
        jetControls.Disable();
    }

    public Vector2 GetMovementDir()
    {
        return jetControls.Jet.Movement.ReadValue<Vector2>();
    }

    public void ShootBtnDown()
    {
        isShoot = true;
    }

    public void ShootBtnUp()
    {
        isShoot = false;
    }
    
    public void RotBtnDown()
    {
        isRot = true;
    }

    public void RotBtnUp()
    {
        isRot = false;
    }

    public void ColorBtn()
    {
        if (colorIndex < playerJetMain.jetColors.Length - 1)
        {
            colorIndex += 1;
        }
        else
        {
            colorIndex = 0;
        }
    }
}
