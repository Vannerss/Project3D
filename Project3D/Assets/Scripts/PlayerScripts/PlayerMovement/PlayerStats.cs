using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int SetHealth(int _healthreduced)
    {
        currentHealth = currentHealth - _healthreduced;
        if (currentHealth <= 0)
        {
            playerIsDead = true;
        }

        return currentHealth;
    }
}
