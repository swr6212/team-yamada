using UnityEngine;
using System.Collections.Generic;

public static class PlayerData
{
    private static int nowMoney;
    private const int maxMoney = 9999999;
    private static int level = 1;
    private static int exp = 0;
    private static List<PlayerStatus> expList;
    public static event System.Action OnLevelUp;

    public static int Money
    {
        get { return nowMoney; }
    }

    public static int Level
    {
        get { return level; }
    }

    public static int Exp
    {
        get { return exp; }
    }

    public static void AddMoney(int amount)
    {
        if(amount <= 0)
        {
            return;
        }
        nowMoney += amount;
        if(nowMoney > maxMoney)
        {
            nowMoney = maxMoney;
        }
    }

    public static void AddExp(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        exp += amount;
        while (expList != null && level < expList.Count && exp >= expList[level - 1].needExp)
        {
            exp -= expList[level - 1].needExp;
            level++;
            OnLevelUp?.Invoke();
        }
    }

    public static bool TrySpend(int amount)
    {
        if(amount <= 0)
        {
            return false;
        }
        if(nowMoney < amount)
        {
            return false;
        }
        else
        {
            nowMoney -= amount;
            return true;
        }
    }

    public static void SetStatusList(List<PlayerStatus> list)
    {
        expList = list;
    }
}
