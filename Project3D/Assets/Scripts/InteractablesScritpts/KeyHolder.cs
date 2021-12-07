using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    public List<Key.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType) 
    { 
        keyList.Remove(keyType); 
    }

    public bool ContainsKeys(Key.KeyType keyType, Key.KeyType keyType1, Key.KeyType keyType2)
    {
        if (keyList.Contains(keyType) && keyList.Contains(keyType2) && keyList.Contains(keyType1)) return true;
        else return false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
    }
}
