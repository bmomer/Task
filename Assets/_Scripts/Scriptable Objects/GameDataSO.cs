using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Game/Data", order = 2)]
public class GameDataSO : ScriptableObject
{
    public int [] playerInventoryItems = new int[50]; //we only store items as amounts, since this is a test game (using their item id's as an index for this array)
    
    private static string savePath => Application.persistentDataPath + "/gamedata.json";

    public void Save()
    {
        File.WriteAllText(savePath, JsonUtility.ToJson(this));
        Debug.Log("GameData saved to " + savePath);
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(savePath), this);
            Debug.Log("GameData loaded, current gold is: " + playerInventoryItems);
        }
    }
}
