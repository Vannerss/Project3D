using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyType
    {
        Red,
        Green,
        Blue
    }

    [SerializeField] 
    private KeyType keyType;

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
