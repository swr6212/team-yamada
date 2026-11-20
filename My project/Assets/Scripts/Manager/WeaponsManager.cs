using UnityEngine;
using System.Collections.Generic;

public static class WeaponsManager
{
    static List<int> weaponList = new List<int>();
    static public void AddWeapon(int weaponId)
    {
        if (!weaponList.Contains(weaponId))
        {
            weaponList.Add(weaponId);
        }
    }

    static public List<int> TakeAll()
    {
        List<int> allWeapons = new List<int>(weaponList);
        weaponList.Clear();
        return allWeapons;
    }

    public static void AllSettingsWeapon(List<int> list)
    {
        if(list == null)
        {
            return;
        }
        weaponList.Clear();
        foreach(int weaponId in list)
        {
            AddWeapon(weaponId);
        }
    }

    public static List<int> AllGetWeapon()
    {
        List<int> saveWeapons = new List<int>(weaponList);
        return saveWeapons;
    }
}
