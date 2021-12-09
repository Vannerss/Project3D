using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        else if(timeRemaining <= 0)
        {
            SceneManager.LoadScene("DieScene");
        }
    }

    public void StartTimer()
    {
        EngGameScene = true;
    }
}
