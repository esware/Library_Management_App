using System.Collections.Generic;
using System.IO;
using DefaultNamespace.DataPersistence.SerializableTypes;
using UnityEngine;

public class AppData:MonoBehaviour
{
    public int deathCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> coinsCollected;
    public AppData()
    {
        deathCount = 0;
        playerPosition = Vector3.zero;
        coinsCollected = new SerializableDictionary<string, bool>();
    }
}