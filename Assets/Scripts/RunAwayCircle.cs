using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RunAwayCircle : MonoBehaviour
{
    public Image bgImage = null;
    public Image progressImage = null;
    public Text timeText = null;
    public Color notInsideColor = Color.white;
    public Color isInsideColor = Color.white;
    public RunAwayObject Object = null;
    public float radius = 0;
    public float totalTime = 200000;
    public float keepInCircleSeconds = 3;
    private float _gatheredSeconds = 0;
    public float gatheredSeconds
    {
        get
        {
            return _gatheredSeconds;
        }
        set
        {
            if (_gatheredSeconds != value)
            {
                _gatheredSeconds = value;
                progressImage.fillAmount = Mathf.Min(1, _gatheredSeconds / keepInCircleSeconds);
            }
        }
    }
    private float _passedSeconds = 0;
    public float passedSeconds
    {
        get
        {
            return _passedSeconds;
        }
        set
        {
            if (_passedSeconds != value)
            {
                _passedSeconds = value;
                timeText.text = _passedSeconds.ToString();
            }
        }
    }
    public event Action<bool> OnGameFinished = null;

    private bool isStarted = false;

    void Start()
    {
    }

    public async void StartMiniGame(float radius, float velocityAmount, float changeRate = 3, float velocityChangeSpeed = 1, int delay = 0)
    {
        transform.localScale = new Vector3(radius, radius, radius);
        await Task.Delay(delay);
        this.radius = radius;
        Object.Init(velocityAmount, changeRate, velocityChangeSpeed);
        isStarted = true;
    }

    private void GameFinished(bool won)
    {
        isStarted = false;
        OnGameFinished?.Invoke(won);
    }

    void Update()
    {
        if (isStarted)
        {
            passedSeconds += Time.deltaTime;
            if (PointInsideSphere(Object.transform.position, transform.position, radius))
            {
                bgImage.color = isInsideColor;
                gatheredSeconds += Time.deltaTime;
            }
            else
            {
                bgImage.color = notInsideColor;
                gatheredSeconds = 0;
            }
            if (gatheredSeconds >= keepInCircleSeconds)
            {
                GameFinished(true);
            }
            if (passedSeconds >= totalTime)
            {
                GameFinished(false);
            }
        }
    }


    public bool PointInsideSphere(Vector3 point, Vector3 center, float radius)
    {
        return Vector3.Distance(point, center) < radius;
    }
}
