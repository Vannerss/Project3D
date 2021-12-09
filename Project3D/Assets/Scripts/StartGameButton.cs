using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public GameObject controlText;
    public GameObject spotLight;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowControls()
    {
        controlText.SetActive(true);
        spotLight.SetActive(true);
    }
}
