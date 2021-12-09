using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{

    public GameObject PauseMenuobject;
    public GameObject SettingsMenuObject;
    public bool ispaused;
    public AudioMixer audioMixer;
    public Animator PlayerDeathAnimation;
    public PlayerStats PlayerStatsScript;
    public AudioSource playerdeathsound;
    public GameObject quitgamebuttonondeath;
    public GameObject restartgamebuttonondeath;
    // Start is called before the first frame update
    void Start()
    {
        quitgamebuttonondeath.SetActive(false);
        restartgamebuttonondeath.SetActive(false);
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
        if (PlayerStatsScript.playerIsDead == true)
        {
            playerdeathsound.Play();
            OnPlayerDeath();
            
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
    public void OnPlayerDeath()
    {
        
        PlayerDeathAnimation.SetTrigger("isdead");
        quitgamebuttonondeath.SetActive(true);
        restartgamebuttonondeath.SetActive(true);
        StartCoroutine("waitforplayerdeathanimation");
    }
    IEnumerator waitforplayerdeathanimation()
    {
        yield return new WaitForSeconds(5);
        Time.timeScale = 0;
        StopCoroutine("waitforplayerdeathanimation");
    }
    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void SetFullScreenpls(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
}
