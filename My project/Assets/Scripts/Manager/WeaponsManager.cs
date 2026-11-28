using UnityEngine;
using System.Collections.Generic;

public static class WeaponsManager
{
    static List<WandData> weaponList = new List<WandData>();

    static public void AddWeapon(int weaponId)
    {
        if (!weaponList.Exists(w  => w.whichWand == weaponId))
        {
            WandData wand = new WandData();
            wand.whichWand = weaponId;
            weaponList.Add(wand);
        }
            
    }

    static public List<WandData> TakeAll()
    {
        List<WandData> allWeapons = new List<WandData>(weaponList);
        weaponList.Clear();
        return allWeapons;
    }

    public static void AllSettingsWeapon(List<WandData> list)
    {
        if(list == null)
        {
            return;
        }
        weaponList.Clear();
        foreach(WandData wand in list)
        {
            weaponList.Add(wand);
        }
    }

    public static List<WandData> AllGetWeapon()
    {
        List<WandData> saveWeapons = new List<WandData>(weaponList);
        return saveWeapons;
    }
}
