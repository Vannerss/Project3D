using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class PlayerStats : MonoBehaviour
{

    public int PlayerHealth = 5;
    int currentHealth;

    public bool playerIsDead = false;

    private void Start()
    {
        currentHealth = PlayerHealth;
        playerIsDead = false;
    }

    public void SetHealth(int _healthreduced)
    {
        currentHealth = currentHealth - _healthreduced;
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("DieScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SetHealth(1);
        }
    }
}
