 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI gameTimeText;

    private float startTime;

    void Start()
    {
        startTime = Time.time; 
    }
    // Update is called once per frame
    void Update()
    {
        float time = Time.time - startTime;

        
        string minutes = ((int)time / 60).ToString();
        string seconds = Mathf.Floor((time % 60)).ToString();
        

        gameTimeText.text = minutes + ":" + seconds;
    }
}
