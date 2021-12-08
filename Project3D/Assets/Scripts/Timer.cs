using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timetTxt;
    public float timeRemaining = 10;
    public bool EngGameScene = false;

    void Update()
    {
        if (timeRemaining > 0 && EngGameScene == true)
        {
            timeRemaining -= Time.deltaTime;
            timetTxt.text = timeRemaining.ToString();
        }
    }

    public void StartTimer()
    {
        EngGameScene = true;
    }
}
