using UnityEngine;

public static class PlayerData
{
    private static int nowMoney;
    private const int maxMoney = 9999999;
    public static int Money
    {
        get { return nowMoney; }
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
