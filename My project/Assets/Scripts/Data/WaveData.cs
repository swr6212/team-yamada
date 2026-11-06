using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveData
{
    public float waveDuration; // ウェーブの長さ（秒）
    public float spawnInterval; // 敵の出現間隔
    public int enemyMinRate; // 敵の最小出現数
    public int enemyMaxRate; // 敵の最大出現数
    public List<EnemyData> enemyDatas; // 敵のデータリスト
}
