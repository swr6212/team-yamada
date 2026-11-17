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
}
