using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("アイテムの識別ID")]
    public string itemId;

    [Header("アイテムの効果の種類")]
    public ItemEffectType effectType;

    [Header("効果の大きさ(例: 攻撃力＋5なら5)")]
    public int effectValue;

    //アイテムの効果の種類をわかりやすく定義
    public enum ItemEffectType
    {
        AttackUp,      //攻撃力アップ
        DefenseUp,     //防御力アップ
        MaxHPUp,       //最大HPアップ

    }
}
