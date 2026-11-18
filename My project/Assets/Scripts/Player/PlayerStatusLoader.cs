using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class PlayerStatusLoader
{
    public static List<PlayerStatus> Load(TextAsset csv)
    {
        List<PlayerStatus> playerStatusList = new List<PlayerStatus>();
        string[] lines = csv.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Trim().Split(',');
            if (values.Length >= 4)
            {
                PlayerStatus playerStatus = new PlayerStatus
                {
                    id = int.Parse(values[0], CultureInfo.InvariantCulture),
                    attack = int.Parse(values[1], CultureInfo.InvariantCulture),
                    hp = int.Parse(values[2], CultureInfo.InvariantCulture),
                    needExp = int.Parse(values[3], CultureInfo.InvariantCulture)
                };
                playerStatusList.Add(playerStatus);
            }
        }
        return playerStatusList;
    }
}