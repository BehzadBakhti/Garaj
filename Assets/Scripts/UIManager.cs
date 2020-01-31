using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    public Text CoinText = null;
    private int _coins = 0;
    public int Coins
    {
        get
        {
            return _coins;
        }
        private set
        {
            if (_coins != value)
            {
                _coins = value;
            }
        }
    }
    public HealthBar healthBar = null;
    public int Health
    {
        get
        {
            return healthBar.CurrentHealth;
        }
    }
    public Image disasterImage = null;
    public Text disasterDesc = null;
    public Animator disasterWindowAnimator = null;
    public RunAwayCircle runAwayCircle = null;
    public SmoothOrbitCam orbitCam = null;

    private void Awake()
    {
        Instance = this;
        runAwayCircle.OnGameFinished += OnMiniGameFinished;
    }

    void Start()
    {
        healthBar.Reset(DataManager.Instance.earthInitialHealth);
    }

    public void AddCoin(int coin)
    {
        Coins += coin;
    }
    public void RemoveCoin(int coin)
    {
        Coins -= coin;
    }

    public void HealthDamage(int damage)
    {
        healthBar.Damage(damage);
    }
    public void ResetHealth()
    {
        healthBar.Reset(DataManager.Instance.earthInitialHealth);
    }

    public void ShowDisaster(DisasterType type)
    {
        var info = DataManager.Instance.GetInfo(type);
        disasterImage.sprite = info.icon;
        disasterDesc.text = info.desc;
        disasterWindowAnimator.SetBool("Show", true);
        runAwayCircle.StartMiniGame(info.minigame_velocity, info.minigame_changeRate, info.minigame_changeSpeed, 3000);
        orbitCam.enabled = false;
    }
    public void HideDisaster()
    {
        disasterWindowAnimator.SetBool("Show", false);
        orbitCam.enabled = true;
    }

    [ContextMenu("Test_ShowDisaster")]
    public void TestShowDisaster()
    {
        ShowDisaster(DisasterType.OilLeakage);
    }
    [ContextMenu("Test_HideDisaster")]
    public void TestHideDisaster()
    {
        HideDisaster();
    }

    private void OnMiniGameFinished(bool done)
    {
        if (done)
        {
            HideDisaster();
        }
    }

}
