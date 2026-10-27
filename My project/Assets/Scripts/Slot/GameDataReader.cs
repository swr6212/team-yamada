
using System.Collections.Generic;
using UnityEngine;

public class GameDataReader : MonoBehaviour
{
    // CSVの各行をIDで管理する
    private Dictionary<string, GameData> dataTable =
        new Dictionary<string, GameData>();

    [System.Serializable]
    public class GameData
    {
        public string id;
        public int hp;
        public int attack;
        public float speed;

        public GameData(string id, int hp, int attack, float speed)
        {
            this.id = id;
            this.hp = hp;
            this.attack = attack;
            this.speed = speed;
        }
    }

    private void Awake()
    {
        LoadCSV();
    }

    // CSVファイルを読み込む
    public void LoadCSV()
    {
        dataTable.Clear();

        // ResourcesフォルダからCSVを読み込む
        TextAsset csvFile = Resources.Load<TextAsset>("GameData");

        if (csvFile == null)
        {
            Debug.LogError(
                "GameData.csvが見つかりません。" +
                "Assets/Resources/GameData.csv に配置してください。"
            );
            return;
        }

        string[] lines = csvFile.text.Split(
            new[] { '\r', '\n' },
            System.StringSplitOptions.RemoveEmptyEntries
        );

        // 1行目は見出しなのでスキップ
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            if (values.Length < 4)
            {
                Debug.LogWarning(
                    $"CSVのデータ形式が不正です。行番号: {i + 1}"
                );
                continue;
            }

            string id = values[0].Trim();

            if (!int.TryParse(values[1].Trim(), out int hp) ||
                !int.TryParse(values[2].Trim(), out int attack) ||
                !float.TryParse(
                    values[3].Trim(),
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float speed))
            {
                Debug.LogWarning(
                    $"CSVの数値が不正です。行番号: {i + 1}"
                );
                continue;
            }

            if (dataTable.ContainsKey(id))
            {
                Debug.LogWarning($"IDが重複しています: {id}");
                continue;
            }

            GameData data = new GameData(id, hp, attack, speed);
            dataTable.Add(id, data);
        }

        Debug.Log($"CSV読み込み完了！ 登録数: {dataTable.Count}");
    }

    // IDからデータを取得する
    public GameData GetData(string id)
    {
        if (dataTable.TryGetValue(id, out GameData data))
        {
            return data;
        }

        Debug.LogWarning($"データが見つかりません: {id}");
        return null;
    }

    // 動作確認
    [ContextMenu("CSVデータ確認")]
    public void TestData()
    {
        GameData player = GetData("Player");

        if (player != null)
        {
            Debug.Log(
                $"プレイヤー HP:{player.hp} " +
                $"攻撃力:{player.attack} " +
                $"移動速度:{player.speed}"
            );
        }
    }
}
