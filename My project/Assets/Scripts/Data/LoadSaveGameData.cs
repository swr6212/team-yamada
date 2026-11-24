using UnityEngine;
using System.IO;
using System.Collections.Generic;


public static class LoadSaveGameData
{
    private static string path = Path.Combine(Application.persistentDataPath, "LoadSaveGameData.json");
    public static void SaveGame()
    {
        SaveData saveData = new SaveData();
        saveData.money = PlayerData.Money;
        saveData.level = PlayerData.Level;
        saveData.exp = PlayerData.Exp;
        saveData.wands = WeaponsManager.AllGetWeapon();
        string save = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, save);
    }

    public static void LoadGame()
    {
        if (!File.Exists(path))
        {
            return;
        }
        string leadFile = File.ReadAllText(path);
        SaveData leadData = JsonUtility.FromJson<SaveData>(leadFile);
        if (leadData == null)
        {
            return;
        }
        PlayerData.LoadData(leadData.money, leadData.level, leadData.exp);
        WeaponsManager.AllSettingsWeapon(leadData.wands);
    }

    public static void DeleteGame()
    { 
        if(!File.Exists(path))
        {
            return;
        }
        else
        {
            File.Delete(path);
        }
    }

    public static bool SaveAvailability()
    {
        return File.Exists(path);
    }
}
