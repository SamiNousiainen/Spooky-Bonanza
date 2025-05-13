using System.Collections.Generic;

/// <summary>
/// Class for storing runtime inventory data
/// </summary>
[System.Serializable]
public class InventoryData {

    public int candyCount;
    public List<string> collectedPumpkins = new List<string>(); //use unique IDs for pumpkin collectibles
}
