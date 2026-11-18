using UnityEngine;

public static class PlayerData
{
    private static int nowMoney;
    private const int maxMoney = 9999999;
    private static int level = 1;

    public static int Money
    {
        get { return nowMoney; }
    }

    public static int Level
    {
        get { return level; }
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
}
