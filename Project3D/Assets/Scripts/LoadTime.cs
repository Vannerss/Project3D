using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class LoadTime : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;

    private void Start()
    {
        timerTxt.text = File.ReadAllText("Assets/save.txt");
    }
}
