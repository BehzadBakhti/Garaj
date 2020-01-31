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

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    public void AddCoin(int coin)
    {
        Coins += coin;
    }
    public void RemoveCoin(int coin)
    {
        Coins -= coin;
    }

    public void ShowDisaster()
    {

    }
}
