using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class for storing runtime player data
/// </summary>
[System.Serializable]
public class PlayerData {

    public int candyCount;
    public List<string> collectedPumpkins = new List<string>(); //use unique IDs for pumpkin collectibles

    public string checkpointID;
    public string sceneName;

    public int maxHealth;
}