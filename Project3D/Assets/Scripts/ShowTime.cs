using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTime : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro.text = PlayerPrefs.GetString("Last Achieved Time");
    }

    void setTimes()
    {
        textMeshPro.text = PlayerPrefs.GetString("Last Achieved Time");
        Debug.Log(PlayerPrefs.GetString("LastAchievedTime"));
    }
}
