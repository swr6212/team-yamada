using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class EnemyStatusLoader
{
    public static List<EnemyStatus> Load(TextAsset csv)
    {
        List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();
        string[] lines = csv.text.Split('\n');
        for(int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Trim().Split(',');
            if (values.Length >= 6)
            {
                EnemyStatus enemyStatus = new EnemyStatus
                {
                    id = int.Parse(values[0], CultureInfo.InvariantCulture),
                    wave = int.Parse(values[1], CultureInfo.InvariantCulture),
                    attack = int.Parse(values[2], CultureInfo.InvariantCulture),
                    hp = int.Parse(values[3], CultureInfo.InvariantCulture),
                    dropExp = int.Parse(values[4], CultureInfo.InvariantCulture),
                    dropCoin = int.Parse(values[5], CultureInfo.InvariantCulture)
                };
                enemyStatusList.Add(enemyStatus);
            }
        }
        return enemyStatusList;
    }
}
