using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class EnemyStatsLoader
{
    public static List<EnemyStats> Load(TextAsset csv)
    {
        List<EnemyStats> enemyStatsList = new List<EnemyStats>();
        string[] lines = csv.text.Split('\n');
        for(int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Trim().Split(',');
            if (values.Length >= 5)
            {
                EnemyStats enemyStats = new EnemyStats
                {
                    name = values[0],
                    hp = int.Parse(values[1], CultureInfo.InvariantCulture),
                    moveSpeed = float.Parse(values[2], CultureInfo.InvariantCulture),
                    damage = int.Parse(values[3], CultureInfo.InvariantCulture),
                    stopDuration = float.Parse(values[4], CultureInfo.InvariantCulture)
                };
                enemyStatsList.Add(enemyStats);
            }
        }
        return enemyStatsList;
    }
}
