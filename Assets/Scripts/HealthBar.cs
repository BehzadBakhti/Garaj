using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public Text healthText = null;
    public Image ProgressBG = null;
    public int Health = 1000;
    private int _CurrentHealth = 0;
    public int CurrentHealth
    {
        get
        {
            return _CurrentHealth;
        }
        private set
        {
            if (_CurrentHealth != value)
            {
                _CurrentHealth = value;
                if (DisplayCurrentHealthTween != null && DisplayCurrentHealthTween.IsPlaying())
                    DisplayCurrentHealthTween.Kill();
                float t = Mathf.Abs(CurrentHealth - DisplayCurrentHealth) / 100;
                t = Mathf.Min(t, 0.2f);
                DisplayCurrentHealthTween = DOTween.To(() => DisplayCurrentHealth, (v) => DisplayCurrentHealth = v, _CurrentHealth, t).SetEase(Ease.OutCirc).OnComplete(()=>
                {
                    DisplayCurrentHealthTween = null;
                });
            }
        }
    }

    private int _DisplayCurrentHealth = 0;
    private Tweener DisplayCurrentHealthTween = null;
    private int DisplayCurrentHealth
    {
        get
        {
            return _DisplayCurrentHealth;
        }
        set
        { 
            if (_DisplayCurrentHealth != value)
            {
                _DisplayCurrentHealth = value;
                healthText.text = _DisplayCurrentHealth.ToString();
                ProgressBG.fillAmount = (float)_DisplayCurrentHealth / (float)Health;
            }
        }
    }

    private void Start()
    {
        CurrentHealth = Health;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Damage(100);
        }
    }
    public void Damage(int damage)
    {
        transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            transform.DOScale(1, 0.1f).SetEase(Ease.InCubic);
        });
        CurrentHealth -= damage;
    }
}
