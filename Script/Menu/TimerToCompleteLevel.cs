using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerToCompleteLevel : MonoBehaviour
{
    private float timer;
    private TimeSpan timePlaying;
    [SerializeField] TextMeshProUGUI timeCouter;

    private void Start()
    {
        timer = 0;
    }
    private void Update()
    {
        TimerInDecimal();
    }
    void TimerInDecimal()
    {
        timer += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(timer);
        string TimeToCompleteLevel = "Time" + timePlaying.ToString("mm':'ss':'ff");
        timeCouter.text = TimeToCompleteLevel;
    }
}
