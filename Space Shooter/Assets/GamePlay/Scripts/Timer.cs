﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 0f;
    public GameObject boss;

    [SerializeField] Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        TimerText.text = currentTime.ToString("0.0");

        if (currentTime <= 0)
            currentTime = 0;
    }
}
