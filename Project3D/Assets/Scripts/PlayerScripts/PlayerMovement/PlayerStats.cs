using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerStats : MonoBehaviour
{

    public int PlayerHealth = 5;
    int currentHealth;

    bool playerIsDead = false;

    private void Start()
    {
        currentHealth = PlayerHealth;
    }

    public int SetHealth(int _healthreduced)
    {
        currentHealth = currentHealth - _healthreduced;
        return currentHealth;
    }
}
