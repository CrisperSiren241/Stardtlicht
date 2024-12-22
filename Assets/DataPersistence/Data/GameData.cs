using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> keysCollected;
    public string Level;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        // PlayerPosX = -27.1f;
        // PlayerPosY = 0.2f;
        // PlayerPosZ = -34.2f;
        playerPosition = new Vector3(-27.1f, 0.2f, -34.2f);
        keysCollected = new SerializableDictionary<string, bool>();
        Level = "Level1";
    }
}