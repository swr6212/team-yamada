using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("ボタンごとにInspectorで設定")]
    public int itemId = 1; //CSVのIDと合わせる（1：火、２：風、３：氷）
    public string itemName = "火"; //確認画面に出す名前

    [Header("CSVから自動で読み込まれる数値")]
    public int attackPower;              // 攻撃力
    public int burnDamage;               // 燃焼ダメージ
    public int bonusDamage;              // 追加ダメージ
    public int speedDownPercent;         // 移動速度低下（%）
    public float duration;               // 効果時間（秒）
}
