 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI gameTimeText;

    private float startTime;
    private bool gameTime = true;

    void Start()
    {
        startTime = Time.time; 
    }
    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }
    void GameTimer()
    {
        if (gameTime)
        {
            float time = Time.time - startTime;


            string minutes = ((int)time / 60).ToString();
            string seconds = Mathf.Floor((time % 60)).ToString();


            gameTimeText.text = minutes + ":" + seconds;
            
        }
    }
    

    public void StopGameTime()
    {
        gameTime = false;
        string gT = "" + gameTimeText.text;
        File.WriteAllText(Application.dataPath + "/save.txt", gT);
    }
}
