using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText = null;
    private float passedTime = 0;
    private bool isTimerRunning = false;
    private float lastSecond = 0;

    void Start()
    {
        StartTime();
    }
    public void StartTime()
    {
        isTimerRunning = true;
    }
    public void ResetTimer()
    {
        passedTime = 0;
        lastSecond = 0;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            passedTime += Time.deltaTime;

            if (passedTime != lastSecond)
            {
                lastSecond = passedTime;
                timerText.text = FormatTime(passedTime);
            }
        }
    }

    private string FormatTime(float seconds)
    {
        int mili = (int)((seconds - (int)seconds) * 100);
        int hour = (int)seconds / 3600;
        seconds -= (hour * 3600);
        int min = (int)seconds / 60;
        seconds -= (min * 60);
        string time = "";
        if (hour > 0)
            time += hour + ":" + FormatNumberLength(min,2) + ":" + FormatNumberLength((int)seconds,2) + ":" + FormatNumberLength(mili, 2);
        else if (min > 0)
            time += FormatNumberLength(min,2) + ":" + FormatNumberLength((int)seconds,2) + ":" + FormatNumberLength(mili, 2);
        else
            time += FormatNumberLength((int)seconds,2) + ":" + FormatNumberLength(mili,2);
        return time;
    }

    private string FormatNumberLength(int num, int length)
    {
        string text = num.ToString();
        for (int i = 0, c = length - text.Length; i < c; i++)
        {
            text = "0" + text;
        }
        return text;
    }
}
