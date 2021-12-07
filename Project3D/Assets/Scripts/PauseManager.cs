using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class PauseManager : MonoBehaviour
{

    public GameObject PauseMenuobject;
    public GameObject SettingsMenuObject;
    public bool ispaused;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispaused)
            {
                ispaused = false;
                Debug.Log("game is not paused");
                PauseMenuobject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                ispaused = true;
                Debug.Log("game is paused");
                PauseMenuobject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ShowSettingsMenu()
    {
        SettingsMenuObject.SetActive(true);
        PauseMenuobject.SetActive(false);
    }
    public void BackButton()
    {
        SettingsMenuObject.SetActive(false);
        PauseMenuobject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenuobject.SetActive(false);
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetFullScreenMode(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
}
